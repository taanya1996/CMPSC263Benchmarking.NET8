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
using System.Data;

BenchmarkRunner.Run<Benchmarks.Benchmark>();

namespace Benchmarks
{
    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.Net80)]
    [SimpleJob(RuntimeMoniker.Net70)]
    [Config(typeof(Config))]
    public class Benchmark
    {
        public class Config : ManualConfig
		{
			public Config() => AddExporter(RPlotExporter.Default);
		}

        [Benchmark]
        public void NullCheck() {
            DataTable dt = null;
            var testDt = new DataTable();
            if(dt != null) {
                testDt = dt;
            }
        }

        [Benchmark]
        public void NullCheckCoalesce() {
            DataTable dt = null;
            var testDt = new DataTable();
            testDt = dt ?? testDt;
        }

        [Benchmark]
        public void ArrayCreationWithCreateInstance() {
            var arr = Array.CreateInstance(typeof(int), 1000);
        }

        [Benchmark]
        public int FindStrings() {
            string longStr = new string('a', 1000);
            longStr += '#';
            return longStr.AsSpan().IndexOfAny(new [] {'b','#'});
        }

        [Benchmark]
        public int FindStrings2() {
            string longStr = new string('a', 1000);
            longStr += '#';
            return longStr.IndexOfAny(new []{'b','#'});
        }

        [Benchmark]
        public void DecodeASCII()
        {
            byte[] bytes = new byte[1000];
            for (int i = 0; i < 1000; i++)
            {
                bytes[i] = (byte)('a' + (i % 26));
            }
            string s = System.Text.Encoding.ASCII.GetString(bytes);
        }

        [Benchmark]
        public void DecodeUTF8()
        {
            byte[] bytes = new byte[1000];
            for (int i = 0; i < 1000; i++)
            {
                bytes[i] = (byte)('a' + (i % 26));
            }
            string s = System.Text.Encoding.UTF8.GetString(bytes);
        }

        [Benchmark]
        public void DecodeDefault()
        {
            byte[] bytes = new byte[1000];
            for (int i = 0; i < 1000; i++)
            {
                bytes[i] = (byte)('a' + (i % 26));
            }
            string s = System.Text.Encoding.Default.GetString(bytes);
        }

        [Benchmark]
        public void EncodeASCII()
        {
            string s = new string('a', 1000);
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(s);
        }

        [Benchmark]
        public void EncodeUTF8()
        {
            string s = new string('a', 1000);
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(s);
        }

        [Benchmark]
        public void EncodeDefault()
        {
            string s = new string('a', 1000);
            byte[] bytes = System.Text.Encoding.Default.GetBytes(s);
        }


    }
}
