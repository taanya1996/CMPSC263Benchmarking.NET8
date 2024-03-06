using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
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
    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.Net80)]
    [Config(typeof(Config))]
    public class Benchmark
    {
        public class Config : ManualConfig
		{
			public Config() => AddExporter(RPlotExporter.Default);
		}

        [Benchmark]
        public SampleStruct Struct() => new SampleStruct(0);

        [Benchmark]
        public SampleClass Class() => new SampleClass(0);

        [Benchmark]
        public IInterfaceForBoxing BoxedStruct() => new BoxedStruct(0);

        [Benchmark]
        public IInterfaceForBoxing ClassFromInterface() => new ClassFromInterface(0);
    }

    public interface IInterfaceForBoxing { }
    public readonly record struct BoxedStruct(int value) : IInterfaceForBoxing;
    public record class ClassFromInterface(int value) : IInterfaceForBoxing;
    public readonly record struct SampleStruct(int value);
    public record class SampleClass(int value);
}
