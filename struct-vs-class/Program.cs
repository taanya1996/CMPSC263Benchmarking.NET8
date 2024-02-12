using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Configs;

BenchmarkRunner.Run<Benchmarks.Benchmark>();

namespace Benchmarks
{
    public readonly record struct SmallStruct( UInt64 A);
    public readonly record struct MediumStruct( UInt64 A,  UInt64 B, string X, string Y);
    public record class SmallClass( UInt64 A);
    public record class MediumClass( UInt64 A,  UInt64 B, string X, string Y);

    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.Net70)]
    [SimpleJob(RuntimeMoniker.Net80)]
    [Config(typeof(Config))]
    public class Benchmark
    {
        public class Config : ManualConfig
		{
			public Config() => AddExporter(RPlotExporter.Default);
		}

        // Since structs are allocated on stack for size < 16B and on heap for size >= 16B, we will benchmark structs with different sizes
        private string _sampleString = new('a', 1000000);
        private UInt64 _a = 1;
        private int rows = 100;

        [Benchmark]
        public void SmallStruct()
        {
            int sum = 0;
            for (int i = 0; i < rows; i++)
            {
                SmallStruct test = new(_a);
            }
        }

        [Benchmark]
        public void MediumStruct()
        {
            for (int i = 0; i < rows; i++)
            {
                MediumStruct test = new(_a, _a, _sampleString, _sampleString);
            }
        }

        [Benchmark]
        public void SmallClass()
        {
            int sum = 0;
            for (int i = 0; i < rows; i++)
            {
                SmallClass test = new(_a);
            }
        }

        [Benchmark]
        public void MediumClass()
        {
            int sum = 0;
            for (int i = 0; i < rows; i++)
            {
                MediumClass test = new(_a, _a, _sampleString, _sampleString);
            }
        }
    }
}
