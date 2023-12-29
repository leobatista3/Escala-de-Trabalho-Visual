using System;
using System.Windows.Forms;

class MainForm : Form
{
    private TextBox horaEntradaTextBox;
    private TextBox horaSaidaTextBox;
    private TextBox terceiroValorTextBox;
    private Button calcularButton;
    private RadioButton adicaoRadioButton;
    private RadioButton subtracaoRadioButton;

    public MainForm()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        // Criar controles do formulário
        horaEntradaTextBox = new TextBox();
        horaSaidaTextBox = new TextBox();
        terceiroValorTextBox = new TextBox();
        calcularButton = new Button();
        adicaoRadioButton = new RadioButton();
        subtracaoRadioButton = new RadioButton();

        // Configurar propriedades dos controles...
        adicaoRadioButton.Text = "Adição";
        subtracaoRadioButton.Text = "Subtração";
        adicaoRadioButton.Checked = true;

        calcularButton.Text = "Calcular";
        calcularButton.Click += CalcularButton_Click;

        // Configurar layout do formulário
        FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
        flowLayoutPanel.Dock = DockStyle.Fill;
        flowLayoutPanel.FlowDirection = FlowDirection.TopDown;

        flowLayoutPanel.Controls.Add(new Label() { Text = "Hora de Entrada:" });
        flowLayoutPanel.Controls.Add(horaEntradaTextBox);
        flowLayoutPanel.Controls.Add(new Label() { Text = "Hora de Saída:" });
        flowLayoutPanel.Controls.Add(horaSaidaTextBox);
        flowLayoutPanel.Controls.Add(new Label() { Text = "Terceiro Valor:" });
        flowLayoutPanel.Controls.Add(terceiroValorTextBox);
        flowLayoutPanel.Controls.Add(new Label() { Text = "Operacao:" });
        flowLayoutPanel.Controls.Add(adicaoRadioButton);
        flowLayoutPanel.Controls.Add(subtracaoRadioButton);
        flowLayoutPanel.Controls.Add(calcularButton);

        Controls.Add(flowLayoutPanel);
    }

    private void CalcularButton_Click(object sender, EventArgs e)
    {
        TimeSpan horaEntrada, horaSaida, terceiroValor;

        if (TimeSpan.TryParse(horaEntradaTextBox.Text, out horaEntrada) &&
            TimeSpan.TryParse(horaSaidaTextBox.Text, out horaSaida) &&
            TimeSpan.TryParse(terceiroValorTextBox.Text, out terceiroValor))
        {
            char opcao = adicaoRadioButton.Checked ? 'A' : 'S';

            // Chame sua função de cálculo aqui
            TimeSpan resultado = opcao == 'A' ? AdicionarHoras(horaEntrada, horaSaida, terceiroValor) : SubtrairHoras(horaEntrada, terceiroValor);
            MessageBox.Show($"Resultado: {resultado}", "Resultado do Calculo");
        }
        else
        {
            MessageBox.Show("Entrada invalida. Certifique-se de inserir tempos validos.", "Erro");
        }
    }

    private TimeSpan AdicionarHoras(TimeSpan horaEntrada, TimeSpan horaSaida, TimeSpan terceiroValor)
    {
        TimeSpan totalHoras = horaSaida - horaEntrada;
        return horaEntrada + totalHoras + terceiroValor;
    }

    private TimeSpan SubtrairHoras(TimeSpan horaEntrada, TimeSpan terceiroValor)
    {
        return horaEntrada - terceiroValor;
    }

    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        // Adicionar menu inicial
        int opcao;
        do
        {
            Console.WriteLine("-------- Menu --------");
            Console.WriteLine("1 - Adicao/Subtracao de Hora");
            Console.WriteLine("2 - Calculo");
            Console.WriteLine("0 - Sair");

            Console.Write("Escolha uma opção: ");
            if (!int.TryParse(Console.ReadLine(), out opcao))
            {
                Console.WriteLine("Opcao inválida. Tente novamente.");
                continue;
            }

            switch (opcao)
            {
                case 1:
                    Application.Run(new MainForm());
                    break;

                case 2:
                    Console.Write("Digite a maior hora: ");
                    if (!TimeSpan.TryParse(Console.ReadLine(), out TimeSpan primeiroValor))
                    {
                        Console.WriteLine("Primeiro valor inválido. Tente novamente.");
                        continue;
                    }

                    Console.Write("Digite a hora que vai ser retirada: ");
                    if (!TimeSpan.TryParse(Console.ReadLine(), out TimeSpan segundoValor))
                    {
                        Console.WriteLine("Segundo valor inválido. Tente novamente.");
                        continue;
                    }

                    TimeSpan terceiroValor = primeiroValor - segundoValor;
                    Console.WriteLine($"--------->> Resultado do calculo: {terceiroValor}");
                    Console.WriteLine("\n\n");
                    break;

                case 0:
                    Console.WriteLine("Saindo do programa.");
                    break;

                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }

        } while (opcao != 0);
    }
}
