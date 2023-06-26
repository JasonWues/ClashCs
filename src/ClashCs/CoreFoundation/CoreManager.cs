using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using ClashCs.Config;
using ClashCs.Tool;

namespace ClashCs.CoreFoundation;

public class CoreManager
{
    private Process process;

    private CoreConfigManager coreConfigManager;
    
    public CoreManager()
    {
        coreConfigManager = Application.Current.GetService<CoreConfigManager>();
    }

    public async Task LoadCore(LocalConfig localConfig)
    {
        var defaultProfile = localConfig.ProfileItems.FirstOrDefault(x => x.IsActive);
        if (defaultProfile == null)
        {
            return;
        }
        if (localConfig.EnableTun && !Util.Instance.Value.IsAdministrator())
        {
            return;
        }
        coreConfigManager.GenerateClientConfig(defaultProfile);

    }

    public void CoreStart()
    {
        try
        {
            var p = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    //FileName = fileName,
                    //Arguments = arguments,
                    //WorkingDirectory = Utils.GetConfigPath(),
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    StandardOutputEncoding = Encoding.UTF8,
                    StandardErrorEncoding = Encoding.UTF8
                }
            };

            p.OutputDataReceived += (sender, args) =>
            {
                if (!string.IsNullOrEmpty(args.Data))
                {
                    var msg = args.Data + Environment.NewLine;
                    //TODO 转发到日志
                }
            };

            p.Start();
            p.BeginOutputReadLine();
            process = p;

            if (p.WaitForExit(1000))
            {
                throw new Exception(p.StandardError.ReadToEnd());
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
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