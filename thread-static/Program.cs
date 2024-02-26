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
    [SimpleJob(RuntimeMoniker.Net70)]
    [SimpleJob(RuntimeMoniker.Net80)]
    [Config(typeof(Config))]
    public class Benchmark
    {
        public class Config : ManualConfig
		{
			public Config() => AddExporter(RPlotExporter.Default);
		}

        [ThreadStatic]
        private static int thread_static_field;

        [Benchmark]
        public void ThreadStatic()
        {
            thread_static_field = 0;
            for (int i = 0; i < 1000000; i++)
            {
                thread_static_field++;
            }
        }
    }
}
