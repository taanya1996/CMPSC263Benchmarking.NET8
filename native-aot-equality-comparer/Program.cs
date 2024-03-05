using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Configs;

BenchmarkRunner.Run<Benchmarks.Benchmark>();
BenchmarkSwitcher.FromAssembly(typeof(Benchmarks.Benchmark).Assembly).Run(args);

namespace Benchmarks
{
    // Alternatively, run : dotnet run -c Release -f net7.0 --filter "*" --runtimes nativeaot7.0 nativeaot8.0
    [Config(typeof(Config))]
    public class Benchmark
    {
        public class Config : ManualConfig
		{
			public Config() => AddExporter(RPlotExporter.Default);
		}

        private readonly int[] _array = Enumerable.Range(0, 1000).ToArray();

        [Benchmark]
        public int GetArrayIndex() => GetArrayIndex(_array, 999);

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static int GetArrayIndex<T>(T[] array, T value)
        {
            for (int i = 0; i < array.Length; i++)
                if (EqualityComparer<T>.Default.Equals(array[i], value))
                    return i;

            return -1;
        }
    }
}
