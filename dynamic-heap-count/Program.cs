using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Threading;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Configs;

BenchmarkRunner.Run<Benchmarks.Benchmark>();

namespace Benchmarks{
    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.Net70)]
    [SimpleJob(RuntimeMoniker.Net80)]
    public class Benchmark{

            [Benchmark]
            public void ThreadedWorkload(){
                const int numberOfThreads = 32;
                var threads = new Thread[numberOfThreads];
                 for (int i = 0; i < numberOfThreads; i++){
                    threads[i] = new Thread(() =>
                    {
                        // Simulate workload here, e.g., creating arrays, performing calculations
                        var data = new byte[10000];
                        for (int j = 0; j < 10000; j++)
                        {
                            // Perform some operations with the data
                            data[i] = 1;
                        }
                    })
                    { IsBackground = true };
                    threads[i].Start();
                }
                foreach (var thread in threads){
                    thread.Join(); // Wait for all threads to complete
                }
            }
    }

}