using ArmyRoster.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace ArmyRoster.Service
{
    internal class TransferService
    {
        public void SelectArmy(ref byte[] arm, string path)
        {

            if (path != null)
            {
                arm = File.ReadAllBytes(path);
            }
        }

        public async Task<bool> TransferArmy(string ipAdress, string port, byte[] arm)
        {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                IPAddress ip = IPAddress.Parse(ipAdress);
                IPEndPoint endpoint = new IPEndPoint(ip, int.Parse(port));
                try
                {
                    await socket.ConnectAsync(endpoint);
                    if (socket.Connected)
                    {
                        // Предполагаем, что arm - это массив байтов, содержащий данные файла
                        // Сначала отправляем размер файла
                        byte[] sizeInfo = BitConverter.GetBytes(arm.Length);
                        await socket.SendAsync(sizeInfo, SocketFlags.None);

                        // Затем отправляем сам файл
                        await socket.SendAsync(arm, SocketFlags.None);
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            return false;
        }

        public async Task<bool> SummonArmy(string ipAdress, string port)
        {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                IPAddress ip = IPAddress.Parse(ipAdress);
                IPEndPoint endpoint = new IPEndPoint(ip, int.Parse(port));

                socket.Bind(endpoint);
                socket.Listen(2);

                try
                {
                    using (Socket ns = await socket.AcceptAsync())
                    {

                        // Получаем размер файла
                        byte[] sizeInfo = new byte[4];
                        await ns.ReceiveAsync(sizeInfo, SocketFlags.None);
                        int fileSize = BitConverter.ToInt32(sizeInfo, 0);

                        // Читаем содержимое файла из сокета
                        byte[] fileData = new byte[fileSize];
                        int totalRead = 0;
                        while (totalRead < fileSize)
                        {
                            int read = await ns.ReceiveAsync(
                                new ArraySegment<byte>(fileData,
                                                        totalRead,
                                                        fileSize - totalRead),
                                SocketFlags.None
                                );
                            if (read == 0)
                            {
                                throw new Exception("Соединение закрыто до окончания передачи файла.");
                            }
                            totalRead += read;
                        }

                        var dialog = new Microsoft.Win32.SaveFileDialog();
                        if (dialog.ShowDialog() == true)
                        {
                            // Сохраняем файл
                            File.WriteAllBytes(dialog.FileName, fileData);
                        }

                        ns.Shutdown(SocketShutdown.Both);
                        ns.Close();
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
                return true;
            }
        }
    }
}
