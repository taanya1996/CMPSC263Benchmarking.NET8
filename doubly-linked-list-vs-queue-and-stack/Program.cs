using System.Collections.Generic;
using System.Linq;
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
        private readonly IEnumerable<int> _list = new List<int>();
        private readonly IEnumerable<int> _stack = new Stack<int>();
        private readonly IEnumerable<int> _queue = new Queue<int>();

        private int[] _data;

        [Params(10, 100, 500)]
        public int Count { get; set; }

        [GlobalSetup]
        public void GlobalSetup()
        {
            _data = Enumerable.Range(0, Count).ToArray();
        }

        // Performance improvement - https://devblogs.microsoft.com/dotnet/performance-improvements-in-net-8/#collections
        [Benchmark]
        public IEnumerator<int> ListCreateEmpty()
        {
            return _list.GetEnumerator();
        }

        [Benchmark]
        public IEnumerator<int> StackCreate()
        {
            return _stack.GetEnumerator();
        }

        [Benchmark]
        public IEnumerator<int> QueueCreate()
        {
            return _queue.GetEnumerator();
        }

        [Benchmark]
        public int ListWrite()
        {
            List<int> list = new(Count);
            Fill(list);

            return list.Count;
        }

        [Benchmark]
        public int QueueWrite()
        {
            Queue<int> queue = new(Count);
            foreach (int i in _data) { queue.Enqueue(i); }
            return queue.Count;
        }

        [Benchmark]
        public int StackWrite()
        {
            Stack<int> stack = new(Count);
            foreach (int i in _data) { stack.Push(i); }
            return stack.Count;
        }

        [Benchmark]
        public void ListRemoveFromEnd()
        {
            List<int> list = new(Count);
            Fill(list);
            while (list.Count > 0) { list.RemoveAt(list.Count - 1); }
        }

        [Benchmark]
        public void ListRemoveFromFront()
        {
            List<int> list = new(Count);
            Fill(list);
            while (list.Count > 0) { list.RemoveAt(0); }
        }

        [Benchmark]
        public void QueueDequeue()
        {
            Queue<int> queue = new(Count);
            foreach (int i in _data) { queue.Enqueue(i); }
            while (queue.Count > 0) { queue.Dequeue(); }
        }

        [Benchmark]
        public void StackPop()
        {
            Stack<int> stack = new(Count);
            foreach (int i in _data) { stack.Push(i); }
            while (stack.Count > 0) { stack.Pop(); }
        }

        public void Fill(ICollection<int> source)
        {
            foreach (int i in _data) { source.Add(i); }
        }

    }
}