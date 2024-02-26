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
    // Alternatively, run : dotnet run -c Release -f net7.0 --filter "*" --runtimes nativeaot7.0 nativeaot8.0
    [SimpleJob(NativeAotRuntime.Net70)]
    [SimpleJob(NativeAotRuntime.Net80)]
    [Config(typeof(Config))]
    public class Benchmark
    {
        public class Config : ManualConfig
		{
			public Config() => AddExporter(RPlotExporter.Default);
		}

        private readonly int[] sample_array = Enumerable.Range(0, 1000).ToArray();

        [Benchmark]
        public int CompareEqualityComparerPerf() => FindValue();

        [MethodImpl(MethodImplOptions.NoInlining)]
        public int FindValue() {
            var comparer = EqualityComparer<int>.Default;
            var result = 0;
            for (int i = 0; i < sample_array.Length; i++)
            {
                if (comparer.Equals(sample_array[i], i))
                    result++;
            }
            return result;
        }
    }
}
