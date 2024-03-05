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

    [Benchmark] 
    public async Task<TimeSpan> ZeroTimeSpan() => TimeSpan.Zero;

    [Benchmark] 
    public async Task<DateTime> MinDateTime() => DateTime.MinValue;

    [Benchmark] 
    public async Task<Guid> EmptyGuid() => Guid.Empty;

    [Benchmark] 
    public async Task<DayOfWeek> Sunday() => DayOfWeek.Sunday;

    [Benchmark] 
    public async Task<float> ZeroFloat() => 0;

    [Benchmark] 
    public async Task<(int, int)> ZeroValueTuple() => (0, 0);

    [Benchmark]
    public async Task<int> ZeroInt() => 0;

    [Benchmark]
    public async Task<string> EmptyString() => string.Empty;

    [Benchmark]
    public async Task<object> NullObject() => null;

    [Benchmark]
    public async Task<List<int>> EmptyList() => new List<int>();
}
