using prime_factorize;
using System;
using System.Diagnostics;

namespace PrimeFactorize
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            numericUpDown1.Value = 930910912222;
            //numericUpDown1.Value = 81818181818919198L;
        }

        private void factorBtn_Click(object sender, EventArgs e)
        {
            long input = (long)numericUpDown1.Value;
            Stopwatch sw = new Stopwatch();

            long[] consume;

            sw.Start();
            List<long> fs = Pollards_Rho_Long.Factorize(input, out consume);
            sw.Stop();

            resultDialogBox.ResetText();
            resultDialogBox.AppendText($"The factors of {input} are ");
            long f = fs[0];
            long test = f;
            resultDialogBox.AppendText($"\r\n{f}");
            for (int i = 1; i < fs.Count; i++)
            {
                f = fs[i];
                resultDialogBox.AppendText($"\r\n{f}");
                test *= f;
            }
            resultDialogBox.AppendText($"\r\n= {test} ({(input== test?"Success":"Failure")})");

            resultDialogBox.AppendText($"\r\n\r\nConsume: ({sw.ElapsedMilliseconds} ms)");
            resultDialogBox.AppendText($"\r\n- CircleFinding : {consume[(int)Pollards_Rho_Consume.CircleFinding]} ({100.0 * consume[(int)Pollards_Rho_Consume.CircleFinding]/ consume[(int)Pollards_Rho_Consume.All]} %)");
            resultDialogBox.AppendText($"\r\n- CalcGCD : {consume[(int)Pollards_Rho_Consume.CalcGCD]} ({100.0 * consume[(int)Pollards_Rho_Consume.CalcGCD] / consume[(int)Pollards_Rho_Consume.All]} %)");
            resultDialogBox.AppendText($"\r\n- PrimalityTest : {consume[(int)Pollards_Rho_Consume.PrimalityTest]} ({100.0 * consume[(int)Pollards_Rho_Consume.PrimalityTest] / consume[(int)Pollards_Rho_Consume.All]} %)");
            resultDialogBox.AppendText($"\r\n- Modular Multiplication : {consume[(int)Pollards_Rho_Consume.MultiplyModFunction]} ({100.0 * consume[(int)Pollards_Rho_Consume.MultiplyModFunction] / consume[(int)Pollards_Rho_Consume.All]} %)");
            resultDialogBox.AppendText($"\r\n- Modular exponentiation : {consume[(int)Pollards_Rho_Consume.PowerModFunction]} ({100.0 * consume[(int)Pollards_Rho_Consume.PowerModFunction] / consume[(int)Pollards_Rho_Consume.All]} %)");
            //resultDialogBox.AppendText($"\r\n- Common : {consume[(int)Pollards_Rho_Consume.Common]} ({100.0 * consume[(int)Pollards_Rho_Consume.Common] / consume[(int)Pollards_Rho_Consume.All]} %)");
            resultDialogBox.AppendText($"\r\n- Total : {consume[(int)Pollards_Rho_Consume.All]}");
        }
    }
}
