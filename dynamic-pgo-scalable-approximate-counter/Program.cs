// See https://aka.ms/new-console-template for more information
using System.Diagnostics;


public class ScalableAppCounter{
    
    private static uint counter = 0;
    private const int ItersPerThread = 100_000_000;

    public static void Main(){ 
        int j = 0;               
        while (j<5)
        {
            Run("Interlock", _ => { for (int i = 0; i < ItersPerThread; i++) Interlocked.Increment(ref counter); });
            Run("Racy     ", _ => { for (int i = 0; i < ItersPerThread; i++) counter++; });
            Run("Scalable ", _ => { for (int i = 0; i < ItersPerThread; i++) Count(ref counter); });
            Console.WriteLine();
            j++;
        }
    }

    private static void Run(string name, Action<int> body){
        counter = 0;
        long start = Stopwatch.GetTimestamp();
        Parallel.For(0, Environment.ProcessorCount, body);
        long end = Stopwatch.GetTimestamp();
        Console.WriteLine($"{name} => Expected: {Environment.ProcessorCount * ItersPerThread:N0}, Actual: {counter,13:N0}, Elapsed: {Stopwatch.GetElapsedTime(start, end).TotalMilliseconds}ms");
    }

    
    private static void Count(ref uint sharedCounter){
        uint currentCount = sharedCounter, delta = 1;
        if (currentCount > 0)
        {
            int logCount = 31 - (int)uint.LeadingZeroCount(currentCount);
            if (logCount >= 13)
            {
                delta = 1u << (logCount - 12);
                uint random = (uint)Random.Shared.NextInt64(0, uint.MaxValue + 1L);
                if ((random & (delta - 1)) != 0)
                {
                    return;
                }
            }
        }

        Interlocked.Add(ref sharedCounter, delta);
    }

} 



