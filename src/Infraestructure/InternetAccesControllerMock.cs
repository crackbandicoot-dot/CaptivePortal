using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
namespace Infraestructure
{
    public class InternetAccesControllerMock : IInternetAccesController
    {
        public async Task AllowTraffic(string ip)
        { 
            string mac = await GetMac(ip);
            using var allowSendingProcces = new Process();
            allowSendingProcces.StartInfo.FileName = "iptables";
            allowSendingProcces.StartInfo.Arguments = $"-A FORWARD -m mac --mac-source {mac} -j ACCEPT";
            allowSendingProcces.Start();
            using var allowReceivingProcces = new Process();
            allowReceivingProcces.StartInfo.FileName = "iptables";
            allowReceivingProcces.StartInfo.Arguments = $"-A FORWARD -d {ip} -m conntrack --ctstate ESTABLISHED,RELATED -j ACCEPT";
            allowReceivingProcces.Start();
            await allowSendingProcces.WaitForExitAsync();
            await allowReceivingProcces.WaitForExitAsync();
        }

        public async Task BlockTraffic(string ip)
        {
            string mac = await GetMac(ip);
            using var blockSendingProcces = new Process();
            blockSendingProcces.StartInfo.FileName = "iptables";
            blockSendingProcces.StartInfo.Arguments = $"-D FORWARD -m mac --mac-source {mac} -j ACCEPT";
            blockSendingProcces.Start();
            using var blockReceivingProcces = new Process();
            blockReceivingProcces.StartInfo.FileName = "iptables";
            blockReceivingProcces.StartInfo.Arguments = $"-D FORWARD -d {ip} -m conntrack --ctstate ESTABLISHED,RELATED -j ACCEPT";
            blockReceivingProcces.Start();
            await blockSendingProcces.WaitForExitAsync();
            await blockReceivingProcces.WaitForExitAsync();
        }
        private async Task<string> GetMac(string ip)
        {
            using var process = new Process();
            process.StartInfo.FileName = "arp";
            process.StartInfo.Arguments = $"-n {ip}";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;

            process.Start();
            string output = await process.StandardOutput.ReadToEndAsync();
            await process.WaitForExitAsync();
            var secondLine = output.Split('\n')[1];
            var mac = secondLine.Split(' ', StringSplitOptions.RemoveEmptyEntries)[2];
            return mac;
        }
    }
}
