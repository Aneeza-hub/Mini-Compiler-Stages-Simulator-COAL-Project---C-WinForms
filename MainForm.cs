using System;
using System.Drawing;
using System.Windows.Forms;

namespace MiniCompilerSimulator
{
    public class MainForm : Form
    {
        private TextBox sourceCodeBox;
        private ComboBox languageBox;
        private TabControl tabControl;

        private DataGridView tokenGrid, symbolGrid;
        private RichTextBox syntaxOutput, parseTreeOutput, icOutput;

        private Button analyzeBtn, clearBtn;

        public MainForm()
        {
            this.Text = "Mini Compiler Stages Simulator";
            this.Size = new Size(1100, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(30, 30, 30);
            this.ForeColor = Color.White;

            InitializeUI();
        }

        private void InitializeUI()
        {
            Label title = new Label()
            {
                Text = "Mini Compiler Stages Simulator",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 10),
                AutoSize = true
            };
            Controls.Add(title);

            languageBox = new ComboBox()
            {
                Location = new Point(850, 15),
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            languageBox.Items.AddRange(new string[] { "C++", "Python" });
            languageBox.SelectedIndex = 0;
            Controls.Add(languageBox);

            sourceCodeBox = new TextBox()
            {
                Multiline = true,
                Font = new Font("Consolas", 11),
                BackColor = Color.Black,
                ForeColor = Color.White,
                Size = new Size(1040, 180),
                Location = new Point(20, 50),
                ScrollBars = ScrollBars.Vertical
            };
            Controls.Add(sourceCodeBox);

            analyzeBtn = new Button()
            {
                Text = "Analyze",
                Location = new Point(20, 240),
                Width = 100
            };
            analyzeBtn.Click += Analyze_Click;

            clearBtn = new Button()
            {
                Text = "Clear",
                Location = new Point(140, 240),
                Width = 100
            };
            clearBtn.Click += (s, e) => sourceCodeBox.Clear();

            Controls.Add(analyzeBtn);
            Controls.Add(clearBtn);

            tabControl = new TabControl()
            {
                Location = new Point(20, 280),
                Size = new Size(1040, 360)
            };

            tabControl.TabPages.Add(CreateLexicalTab());
            tabControl.TabPages.Add(CreateSyntaxTab());
            tabControl.TabPages.Add(CreateICTab());

            Controls.Add(tabControl);
        }

        private TabPage CreateLexicalTab()
        {
            TabPage tab = new TabPage("Lexical Analysis");

            tokenGrid = new DataGridView()
            {
                Location = new Point(10, 10),
                Size = new Size(500, 300),
                BackgroundColor = Color.Black,
                ForeColor = Color.White,
                AllowUserToAddRows = false
            };

            tokenGrid.Columns.Add("Line", "Line");
            tokenGrid.Columns.Add("Type", "Token Type");
            tokenGrid.Columns.Add("Lexeme", "Lexeme");

            symbolGrid = new DataGridView()
            {
                Location = new Point(520, 10),
                Size = new Size(500, 300),
                BackgroundColor = Color.Black,
                ForeColor = Color.White,
                AllowUserToAddRows = false
            };

            symbolGrid.Columns.Add("Name", "Name");
            symbolGrid.Columns.Add("Type", "Type");
            symbolGrid.Columns.Add("Scope", "Scope");
            symbolGrid.Columns.Add("Line", "Line");

            tab.Controls.Add(tokenGrid);
            tab.Controls.Add(symbolGrid);

            return tab;
        }

        private TabPage CreateSyntaxTab()
        {
            TabPage tab = new TabPage("Syntax Analysis");

            syntaxOutput = new RichTextBox()
            {
                Location = new Point(10, 10),
                Size = new Size(500, 300),
                BackColor = Color.Black,
                ForeColor = Color.LightGreen
            };

            parseTreeOutput = new RichTextBox()
            {
                Location = new Point(520, 10),
                Size = new Size(500, 300),
                BackColor = Color.Black,
                ForeColor = Color.White
            };

            tab.Controls.Add(syntaxOutput);
            tab.Controls.Add(parseTreeOutput);

            return tab;
        }

        private TabPage CreateICTab()
        {
            TabPage tab = new TabPage("Intermediate Code");

            icOutput = new RichTextBox()
            {
                Location = new Point(10, 10),
                Size = new Size(1010, 300),
                BackColor = Color.Black,
                ForeColor = Color.White,
                Font = new Font("Consolas", 11)
            };

            tab.Controls.Add(icOutput);
            return tab;
        }

        private void Analyze_Click(object sender, EventArgs e)
        {
            tokenGrid.Rows.Clear();
            symbolGrid.Rows.Clear();
            syntaxOutput.Clear();
            parseTreeOutput.Clear();
            icOutput.Clear();

            // Demo Lexical Output
            tokenGrid.Rows.Add("1", "Keyword", "for");
            tokenGrid.Rows.Add("1", "Identifier", "i");
            tokenGrid.Rows.Add("1", "Operator", "<");

            symbolGrid.Rows.Add("i", "int", "local", "1");

            // Syntax Output
            syntaxOutput.Text = "✔ Syntax is VALID\nDetected: Loop, Assignment";

            // Parse Tree
            parseTreeOutput.Text =
                "Program\n" +
                " └── ForLoop\n" +
                "     ├── Initialization\n" +
                "     ├── Condition\n" +
                "     └── Body";

            // Intermediate Code
            icOutput.Text =
                "t1 = 0\n" +
                "L1:\n" +
                "if i >= 5 goto L2\n" +
                "t2 = sum + i\n" +
                "sum = t2\n" +
                "i = i + 1\n" +
                "goto L1\n" +
                "L2:";
        }
    }
}
