/// NetMQ Listener class based on a NetMQ example listener class from the following github repo:
/// https://github.com/valkjsaaa/Unity-ZeroMQ-Example

using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    // NetMQ Listener Class
    public class NetMqListener
    {
        private readonly Thread _listenerWorker;

        private bool _listenerCancelled;

        private string _socket;

        private string _topic;

        public delegate void MessageDelegate(byte[] message);

        private readonly MessageDelegate _messageDelegate;

        private readonly ConcurrentQueue<byte[]> _messageQueue = new ConcurrentQueue<byte[]>();

        private void ListenerWork()
        {
            AsyncIO.ForceDotNet.Force();
            using (var subSocket = new SubscriberSocket())
            {
                subSocket.Options.ReceiveHighWatermark = 1000;
                subSocket.Connect(_socket);
                subSocket.Subscribe(_topic);
                Debug.Log("Connected to: " + _socket + " on topic: " + _topic);
                var msg = new Msg();
                msg.InitEmpty();
                var timeout = new TimeSpan(0, 0, 0, 1);
                while (!_listenerCancelled)
                {
                    if (!subSocket.TryReceive(ref msg, timeout)) continue;
                    _messageQueue.Enqueue(msg.Data);
                }
                subSocket.Close();
            }
            NetMQConfig.Cleanup();
        }

        public void Update()
        {
            while (!_messageQueue.IsEmpty)
            {
                byte[] message;
                if (_messageQueue.TryDequeue(out message))
                {
                    _messageDelegate(message);
                }
                else
                {
                    break;
                }
            }
        }

        public NetMqListener(MessageDelegate messageDelegate, string socket, string topic)
        {
            _messageDelegate = messageDelegate;
            _listenerWorker = new Thread(ListenerWork);
            _socket = socket;
            _topic = topic;
        }

        public void Start()
        {
            _listenerCancelled = false;
            _listenerWorker.Start();
        }

        public void Stop()
        {
            _listenerCancelled = true;
            _listenerWorker.Join();
        }
    }
}
