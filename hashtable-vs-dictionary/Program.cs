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

        private Dictionary<int, int> _dictionary;
        private Hashtable _hashtable;
        private HybridDictionary _hybridDictionary;

        private int[] _data;

        [Params(5, 10, 100, 500, 1000, 100000)]
        public int Count { get; set; }

        [GlobalSetup]
        public void GlobalSetup()
        {
            _data = Enumerable.Range(0, Count).ToArray();
        }

        [Benchmark]
        public int DictionaryWrite()
        {
            _dictionary = new Dictionary<int, int>();
            foreach (int i in _data)
            {
                _dictionary.Add(i, i);
            }
            return _dictionary.Count;
        }

        [Benchmark]
        public int HashtableWrite()
        {
            _hashtable = new Hashtable();
            foreach (int i in _data)
            {
                _hashtable.Add(i, i);
            }
            return _hashtable.Count;
        }

        [Benchmark]
        public int HybridDictionaryWrite()
        {
            _hybridDictionary = new HybridDictionary();
            foreach (int i in _data)
            {
                _hybridDictionary.Add(i, i);
            }
            return _hybridDictionary.Count;
        }
    }
}
