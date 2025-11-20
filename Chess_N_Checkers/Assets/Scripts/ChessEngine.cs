using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

public class ChessEngine : MonoBehaviour
{
    private System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
    private List<string> output = new List<string>();
    private List<string> error = new List<string>();
    private StreamWriter myInputWriter;

    private bool isLaunched = false;

    Queue chessCommands = new Queue();
    Queue engineMoves = new Queue();

    private static ManualResetEvent engineThinker = new ManualResetEvent(false);
     
    void Awake()
    {
        myProcess.StartInfo.FileName = "C:\\Software\\JLGames\\stockfish\\stockfish-windows-x86-64-avx2.exe";
        myProcess.StartInfo.UseShellExecute = false;
        myProcess.StartInfo.CreateNoWindow = true;
        myProcess.StartInfo.RedirectStandardOutput = true;
        myProcess.StartInfo.RedirectStandardInput = true;
        myProcess.StartInfo.RedirectStandardError = true;

        myProcess.OutputDataReceived += (sender, args) =>
        {
            if (!string.IsNullOrEmpty(args.Data))
            {
                //UnityEngine.Debug.Log("===>>> " + args.Data);
                if (((string)args.Data).StartsWith("bestmove"))
                {
                    string[] words = ((string)args.Data).Split(' ');
                    UnityEngine.Debug.Log("stockfish replied with move: " + words[1]);
                    engineMoves.Enqueue(words[1]);
                    UnityEngine.Debug.Log("sending thread signal");
                    engineThinker.Set();
                }
            }
        };
        myProcess.ErrorDataReceived += (sender, args) =>
        {
            if (!string.IsNullOrEmpty(args.Data))
            {
                UnityEngine.Debug.Log("ERROR >>> " + args.Data);
            }
        };
        UnityEngine.Debug.Log("Subprocess configured for Stockfish chess engine.");
    }

    // Start is called before the first frame update
    void Start()
    {
        //init commands
        chessCommands.Enqueue("isready");
        chessCommands.Enqueue("d");
        if (!isLaunched)
        {
            myProcess.Start();
            isLaunched = true;
            myInputWriter = new StreamWriter(myProcess.StandardInput.BaseStream, Encoding.ASCII);
            myProcess.BeginOutputReadLine();
            myProcess.BeginErrorReadLine();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (chessCommands.Count > 0)
        {
            string cmd = (string) chessCommands.Dequeue();
            //UnityEngine.Debug.Log("===> Sending command: " + cmd);
            myInputWriter.WriteLineAsync(cmd);
            myInputWriter.FlushAsync(); 
        }
    }

    public async void OnButtonClick()
    {
        //setoption name Threads value 4
        //position startpos
        //position startpos moves e2e4 e7e5 g1f3 b8c6 f1b5
        //position fen 8/1B6/8/5p2/8/8/5Qrq/1K1R2bk w - - 0 1
        //go depth 4

        chessCommands.Enqueue("position startpos moves e2e4 e7e5");

        UnityEngine.Debug.Log("button was clicked");

        CancellationTokenSource tokenSource = new CancellationTokenSource();
        CancellationToken token = tokenSource.Token;
        Task myTask = Task.Run(() =>
        {
            System.Threading.Thread.Sleep(1000);
            UnityEngine.Debug.Log("telling chess engine to start thinking about the best move");
            chessCommands.Enqueue("go depth 4");
        }, token);

        // Start the Task
        //await Task.Yield();
        UnityEngine.Debug.Log("Waiting on response from chess engine.");
        await myTask;

        engineThinker.WaitOne();
        if (engineMoves.Count > 0)
        {
            UnityEngine.Debug.Log("***** The chess engine move is: " + engineMoves.Dequeue());
        }
        else
        {
            UnityEngine.Debug.Log("The chess engine did not provide a move.");
        }
        engineThinker.Reset();
        UnityEngine.Debug.Log("OnButtonClick function complete.");
    }

    void OnDestroy()
    {
        UnityEngine.Debug.Log("OnDestroy Stockfish");
        if (myInputWriter != null)
        {
            myInputWriter.Close();
        }
        isLaunched = false;
        myProcess.Close();    
        UnityEngine.Debug.Log("OnDestroy function complete.");
    }
}