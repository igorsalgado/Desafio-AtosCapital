using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Atividade
{
    public partial class frmAtividade : Form
    {
        public frmAtividade()
        {
            InitializeComponent();
        }



        private void btnRun_Click(object sender, EventArgs e)
        {
            if (txtArquivo.Text.Trim().Equals(""))
            {
                MessageBox.Show(this, "Caminho do arquivo deve ser informado");
                txtArquivo.Focus();
                return;
            }

            if (!File.Exists(txtArquivo.Text.Trim()))
            {
                MessageBox.Show(this, "Arquivo inexistente!");
                txtArquivo.Focus();
                return;
            }

            Thread thread = new Thread(() => ExecutaAtividade(txtArquivo.Text.Trim()));
            thread.Name = "Atividade - Run";
            thread.Start();
        }


        private void ExecutaAtividade(string filePath)
        {
            this.Invoke(new MethodInvoker(delegate()
            {
                txtArquivo.Enabled = false;
                btnRun.Enabled = false;
            }));

            try
            {
                CodigoAtividade(filePath);

                this.Invoke(new MethodInvoker(delegate()
                {
                    MessageBox.Show(this, "Finalizado!");
                }));
            }
            catch (Exception ex)
            {
                this.Invoke(new MethodInvoker(delegate()
                {
                    MessageBox.Show(this, ex.Message);
                }));
            }
            finally
            {
                this.Invoke(new MethodInvoker(delegate()
                {
                    txtArquivo.Enabled = true;
                    btnRun.Enabled = true;
                }));
            }
        }




        private void CodigoAtividade(string filePath)
        {
            // Lê o arquivo de entrada e o converte num array de string
            string[] lines = File.ReadAllLines(filePath);

            // Lê a primeira linha, quebrando ela em informações separadas pelo espaço
            string[] firstLine = lines[0].Split(' ');

            // Primeira informação é a quantidade de linhas
            int linhas = Convert.ToInt32(firstLine[0]);
            // Segunda informação é a quantidade de colunas
            int colunas = Convert.ToInt32(firstLine[1]);

            // Preenche matriz do labirinto
            string[,] matriz = new string[linhas, colunas];
            int lAtual = -1; // Posição inicial: linha
            int cAtual = -1; // Posição inicial: coluna
            int lSaida = -1; // Saída: linha
            int cSaida = -1; // Saída: coluna

            // percorre toda a matriz (a partir da segunda linha do arquivo texto) para identificar a posição inicial e a saída
            for (int l = 1; l < lines.Length; l++)
            {
                string[] line = lines[l].Split(' ');
                for (int c = 0; c < line.Length; c++)
                {
                    string ll = line[c];
                    matriz[l - 1, c] = ll;

                    if (ll.Equals("X"))
                    {
                        // Posição inicial
                        lAtual = l - 1;
                        cAtual = c;
                    }
                    else if (ll.Equals("0") && (l == 1 || c == 0 || l == lines.Length - 1 || c == line.Length - 1))
                    {
                        // Saída
                        lSaida = l - 1;
                        cSaida = c;
                    }
                }
            }

            // Posição máxima de linha e coluna que pode ser movida (borda)
            int extremidadeLinha = linhas - 1;
            int extremidadeColuna = colunas - 1;

            // Guarda o trajeto em uma list de string e já inicia com a posição de origem
            List<string> resultado = new List<string>();
            resultado.Add("O [" + (lAtual + 1) + ", " + (cAtual + 1) + "]");

            // Armazena as posições visitadas em uma pilha
            //Eu utilizei Stack<Tuple<int , int>> pois a pilha é uma estrutura de dados que segue a regra LIFO (Last In First Out), ou seja, o último elemento a entrar é o primeiro a sair.
            //E Tuple<int, int> é uma estrutura de dados que permite armazenar dois valores inteiros em uma única variável.
            Stack<Tuple<int, int>> posVisitada = new Stack<Tuple<int, int>>();
            posVisitada.Push(Tuple.Create(lAtual, cAtual));


            // Percorre a matriz (labirinto) até encontrar a saída, usando as regras de prioridade e posições não visitadas, e vai armazenando o trajeto na list resultado
            bool achouSaida = lAtual == lSaida && cAtual == cSaida;
            while (!achouSaida)
            {
                // Se a posição acima não é parede e não foi visitada
                if (lAtual > 0 && matriz[lAtual - 1, cAtual] != "1" && !posVisitada.Contains(Tuple.Create(lAtual - 1, cAtual)))
                {
                    lAtual--; // Decrementa a linha
                    resultado.Add($"C [{lAtual + 1}, {cAtual + 1}]"); // Adiciona o movimento na list resultado
                    posVisitada.Push(Tuple.Create(lAtual, cAtual)); // Adiciona a posição na pilha de posições visitadas

                }
                // Se a posição a esquerda não é parede e não foi visitada
                else if (cAtual > 0 && matriz[lAtual, cAtual - 1] != "1" && !posVisitada.Contains(Tuple.Create(lAtual, cAtual - 1)))

                {
                    cAtual--;
                    resultado.Add($"E [{lAtual + 1}, {cAtual + 1}]");
                    posVisitada.Push(Tuple.Create(lAtual, cAtual));
                }
                // Se a posição a direita não é parede e não foi visitada
                else if (cAtual < extremidadeColuna && matriz[lAtual, cAtual + 1] != "1" && !posVisitada.Contains(Tuple.Create(lAtual, cAtual + 1)))

                {
                    cAtual++; // Incrementa a coluna
                    resultado.Add($"D [{lAtual + 1}, {cAtual + 1}]");
                    posVisitada.Push(Tuple.Create(lAtual, cAtual));
                }
                // Se a posição abaixo não é parede e não foi visitada
                else if (lAtual < extremidadeLinha && matriz[lAtual + 1, cAtual] != "1" && !posVisitada.Contains(Tuple.Create(lAtual + 1, cAtual)))

                {
                    lAtual++;
                    resultado.Add($"B [{lAtual + 1}, {cAtual + 1}]");
                    posVisitada.Push(Tuple.Create(lAtual, cAtual));

                }
                else
                {
                    // Volta para a posição anterior
                    posVisitada.Pop();

                    // Bloqueia a posição atual para não voltar nela
                    matriz[lAtual, cAtual] = "1";
                }

                // Achou a saída?
                achouSaida = lAtual == lSaida && cAtual == cSaida;
            }

            // Salva arquivo texto de saída com o trajeto
            string folderPath = Path.GetDirectoryName(filePath);
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            File.WriteAllLines(Path.Combine(folderPath, "saida-" + fileName + ".txt"), resultado.ToArray(), Encoding.UTF8);
        }
    }
}
