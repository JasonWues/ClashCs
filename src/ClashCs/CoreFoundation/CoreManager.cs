using System;
using System.Diagnostics;

namespace ClashCs.CoreFoundation;

public class CoreManager
{
    private Process process;

    public CoreManager()
    {
        
    }

    public void CoreStop()
    {
        try
        {
            if (process != null)
            {
                process.Dispose();
                process = null;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    private void KillProcess(Process p)
    {
        try
        {
            p.CloseMainWindow();
            p.WaitForExit(100);
            if (p.HasExited)
            {
                return;
            }
            p.Kill();
            p.WaitForExit(100);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}