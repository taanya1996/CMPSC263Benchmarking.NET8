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

        [Benchmark(Baseline = true)]
        public Task ForEachAsyncTest() => Parallel.ForEachAsync(Enumerable.Range(0, 1_000_000), async (i, count) => await Task.Delay(1));

        [Benchmark]
        public Task ForAsyncTest() => Parallel.ForAsync(0, 1_000_000, async (i, count) => await Task.Delay(1));
    }
}
