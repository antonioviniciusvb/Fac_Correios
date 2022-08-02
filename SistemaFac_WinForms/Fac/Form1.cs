using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;
using C5;
using System.Diagnostics;
using FAC;

namespace fac
{

    public partial class Form1 : Form
    {
        #region Variaveis Globais
        int local = 0, estadual = 0, nacional = 0, cepErrados = 0, total = 0, tipo = 0;
        char del;

        //Tamanhos formatações
        int tamCep = 9, tamDirecaoCep1 = 50, tamDirecaoCep2 = 60, tamNumDirecao = 9, tamCIF = 34, tamCIF_Formatado = 38;
        int tamDataMatrix = 102, tamDtPost = 6, tamCEPNET = 9, tamSeq = 11;

        

        FileInfo fileInfo = null;
        SortedList<string, string> ls = null;
        TreeDictionary<string, string> direcaoCep = null;
        Encoding encoding = Encoding.GetEncoding(CultureInfo.GetCultureInfo("pt-BR").TextInfo.ANSICodePage);
        string curDir = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());
        #endregion

        /// <summary>
        /// Construtor
        /// </summary>
        public Form1()
        {
           InitializeComponent();
           direcaoCep = carregarDirecaoCep();
        }

        private TreeDictionary<string, string> carregarDirecaoCep()
        {
            TreeDictionary<string, string> direcoes = new TreeDictionary<string, string>();
            using (StreamReader str = new StreamReader($"{curDir}\\plano_triagem.txt", encoding))
            {
                string linha = string.Empty;
                while ((linha = str.ReadLine()) != null)
                {
                    var campos = linha.Split(';');
                    direcoes.Add(campos[2], $"{campos[3].PadRight(tamCep)};{campos[4].PadRight(tamCep)};{campos[5].Trim().PadRight(tamDirecaoCep1)};" +
                                            $"{campos[6].Trim().PadRight(tamDirecaoCep2)};{campos[7].PadRight(tamNumDirecao)}");
                }
            }

            return direcoes;
        }

        private void btnSelecionaArq_Click(object sender, EventArgs e)
        {
            if (opfdArq.ShowDialog() == DialogResult.OK && opfdArq.FileName.Length > 0)
            {
                fileInfo = new FileInfo(opfdArq.FileName);
                txBoxArquivo.Text = opfdArq.FileName;
            }
        }

        private void Work()
        {
            limpaEstatisticas();

            string nomeArq = definindoNomeArquivo("Ordenado"), arquivoMidia = definindoNomeArquivoMidia(), arq_Invalidos = definindoNomeArquivo("Invalido");

            int i = 0, registros = 0;
            string cep = string.Empty, numeroEnd = string.Empty;

            #region Capturando Arquivos e Ordenando

            ls = new SortedList<string, string>();

            using (StreamReader str = new StreamReader(fileInfo.FullName, encoding))
            {
                string linha = String.Empty, cabecalho = string.Empty;

                if (cbHeader.Checked)
                {
                    cabecalho = formataLinha(str.ReadLine());
                    ls.Add("0", cabecalho);
                    i++;
                }

                registros = QntdRegistros(fileInfo);

                #region Delimitado
                if (tipo == 0)
                {
                    while ((linha = str.ReadLine()) != null)
                    {
                        i++;

                        linha = formataLinha(linha);

                        if (cbTrailler.Checked && i == registros)
                        {
                            ls.Add("9999999999", linha);
                            break;
                        }

                        var campos = linha.Split('\t');

                        if (npDelCompCepDestino.Value > 0)
                            cep = Correios.Fac.limpaCep($"{campos[Convert.ToInt16(npDelCEPDestino.Value) - 1]}{(campos[Convert.ToInt16(npDelCompCepDestino.Value) - 1])}");
                        else
                            cep = Correios.Fac.limpaCep($"{campos[Convert.ToInt16(npDelCEPDestino.Value) - 1]}");

                        if (npDelNumEndereco.Value > 0)
                            numeroEnd = Correios.Fac.limpaNumero(campos[Convert.ToInt32(npDelNumEndereco.Value) - 1]);
                        else
                            numeroEnd = "00000";

                        ls.Add($"{cep}{numeroEnd}{formatInt(i)}", linha);
                    }
                }

                #endregion

                else
                if (tipo == 1)
                {
                    while ((linha = str.ReadLine()) != null)
                    {
                        i++;

                        linha = formataLinha(linha);

                        if (cbTrailler.Checked && i == registros)
                        {
                            ls.Add("9999999999", linha);
                            break;
                        }

                        cep = Correios.Fac.limpaCep(
                            $"{linha.Substring(Convert.ToInt32(npFixoCepDestino.Value) - 1, Convert.ToInt32(npFixoTamanho.Value))}");

                        if (npFixoNumEnd.Value > 0)
                            numeroEnd = Correios.Fac.limpaNumero(linha.Substring((Convert.ToInt32(npFixoNumEnd.Value) - 1), Convert.ToInt32(npFixoTamanhoNumEnd.Value)));
                        else
                            numeroEnd = "00000";

                        ls.Add($"{cep}{numeroEnd}{formatInt(i)}", linha);
                    }
                }
                else
                if (tipo == 2)
                {
                    StringBuilder auxReg = new StringBuilder();

                    string auxCep = string.Empty;
                    int cont = 0;

                    while ((linha = str.ReadLine()) != null)
                    {
                        cont++;

                        linha = formataLinha(linha);

                        if (linha.Substring(Convert.ToInt32(npMultiPosChave.Value) - 1, txBoxMultiChave.Text.Length) == txBoxMultiChave.Text)
                        {
                            if (auxReg.Length == 0)
                            {
                                i++;
                                auxReg.AppendLine(linha);

                                if (npMultiNumEnd.Value > 0)
                                    numeroEnd = Correios.Fac.limpaNumero(linha.Substring((Convert.ToInt32(npMultiNumEnd.Value) - 1), Convert.ToInt32(npMultiTamanhoEnd.Value)));
                                else
                                    numeroEnd = "00000";

                                auxCep = Correios.Fac.limpaCep(linha.Substring(Convert.ToInt32(npMultiCepDest.Value - 1),
                                              Convert.ToInt32(npMultiTamanhoCepDest.Value)).ToString());
                            }
                            else
                            {

                                ls.Add($"{auxCep}{numeroEnd}{formatInt(i)}", auxReg.ToString());
                                auxReg.Clear();

                                if (npMultiNumEnd.Value > 0)
                                    numeroEnd = Correios.Fac.limpaNumero(linha.Substring((Convert.ToInt32(npMultiNumEnd.Value) - 1), Convert.ToInt32(npMultiTamanhoEnd.Value)));
                                else
                                    numeroEnd = "00000";

                                auxReg.AppendLine(linha);
                                auxCep = Correios.Fac.limpaCep(linha.Substring(Convert.ToInt32(npMultiCepDest.Value - 1),
                                              Convert.ToInt32(npMultiTamanhoCepDest.Value)).ToString());
                                i++;
                            }
                        }
                        else
                        {
                            if (cbTrailler.Checked && cont+1 == registros)
                            {
                                ls.Add($"{auxCep}{numeroEnd}{formatInt(i)}", auxReg.ToString());
                                ls.Add("9999999999", linha);
                                break;
                            }

                            auxReg.AppendLine(linha);
                        }

                        if (cont == registros)
                            ls.Add($"{auxCep}{numeroEnd}{formatInt(i)}", auxReg.ToString());

                        if (!(Regex.IsMatch($"{auxReg}", $"^{txBoxMultiChave.Text}")))
                            throw new Exception("A primeira posição do registro não é a chave."+ Environment.NewLine +
                                                 "É possível que as configurações de Header, Trailer ou Chave estejam erradas.");
                    }
                }
            }

            #endregion

            criaArquivo(nomeArq, arquivoMidia, arq_Invalidos);
            limparMemoria();
            exibirDetalhes();
        }

        private string formataLinha(string linha)
        {
            string auxLinha = string.Empty;

            //Limpa alguns caracteres invalidos
            auxLinha = Regex.Replace(linha, @"[\x00-\x08\x0A\x7F]", " ");

            //Limpa o caractere TAB, caso o tipo de delimitador não seja ele
            auxLinha = tipo == 0 && cbDelimitador.SelectedIndex != 3 ? auxLinha = Regex.Replace(auxLinha, @"\t", " ") : auxLinha;

            //Substitui o delimitador de entrada pelo TAB, caso o tipo do arquivo seja delimitado
            auxLinha = tipo == 0 ? auxLinha = Regex.Replace(auxLinha, $"{del}", "\x09") : auxLinha;

            return auxLinha;
        }

        private void limpaEstatisticas()
        {
            local = 0;
            estadual = 0;
            nacional = 0;
            total = 0;
            cepErrados = 0;
        }

        private static string formatInt(int value)
        {
            return string.Format("{0:00000}", value);
        }

        private int QntdRegistros(FileInfo arq)
        {
            int i = 0;

            using (StreamReader str = new StreamReader(arq.FullName, Encoding.GetEncoding(
               CultureInfo.GetCultureInfo("pt-BR").TextInfo.ANSICodePage)))
            {
                while (str.ReadLine() != null)
                    i++;
            }

            return i;
        }

        private void criaArquivo(string nomeArq, string nomeArqMidia, string nomeArqInvalido)
        {
            string dirCep = string.Empty;
            int pos = 0, seq = 0;

            using (StreamWriter stw = new StreamWriter(nomeArq, false, encoding))
            {
                using (StreamWriter stwMidia = new StreamWriter(nomeArqMidia, false, encoding))
                {
                    using (StreamWriter stwInvalido = new StreamWriter(nomeArqInvalido, false, encoding))
                    {
                        stwMidia.WriteLine($"1{mskDR.Text}{mskCodAdm.Text}{mskNumCartao.Text}{formatInt(Convert.ToInt32(npLote.Value))}" +
                        $"{mskSTO.Text}{Correios.Fac.limpaCep(mskUndPostagem.Text)}{mskNumContrato.Text}");

                        //Imprime Cabecalhos
                        if (cbHeader.Checked)
                        {
                            if (tipo == 0)
                                stw.WriteLine($"{ls.Values[0]}\tDataMatrix\tInicio\tFim\tDirecao_1\tDirecao_2\tN_Direcao\tCif\tCif_Formatado\tCif_128\tDtPost\tCepNet\tSeq");
                            else
                                stw.WriteLine($"{ls.Values[0]}{"DataMatrix".PadRight(tamDataMatrix)}{"Inicio".PadRight(tamCep)}{"Fim".PadRight(tamCep)}" +
                                          $"{"Direcao_1".PadRight(tamDirecaoCep1)}{"Direcao_2".PadRight(tamDirecaoCep2)}{"N_Direcao".PadRight(tamNumDirecao)}" +
                                          $"{"Cif".PadRight(tamCIF)}{"Cif_Formatado".PadRight(tamCIF_Formatado)}{"Cif_128".PadRight(tamCIF)}" +
                                          $"{"DtPost".PadRight(tamDtPost)}{"CepNet".PadRight(tamCEPNET)}{"Seq".PadRight(tamSeq)}");
                            pos++;
                        }

                        for (int i = pos; i < ls.Count; i++)
                        {
                            if (cbTrailler.Checked && i == ls.Count - 1)
                            {
                                stw.WriteLine(ls.Values[i]);
                                break;
                            }

                            var cepDestino = ls.Keys[i].Substring(0, 8);
                            var servico = Correios.Fac.servico(cepDestino);

                            if (servico != "00000")
                            {
                                if (servico == "82015")
                                    local++;
                                else
                                   if (servico == "82023")
                                    estadual++;
                                else
                                    if (servico == "82031")
                                    nacional++;

                                seq++;

                                var numDestino = int.Parse(ls.Keys[i].Substring(8, 5));
                                var cepNet = Correios.Fac.CepNet(cepDestino, 0);
                                var cepRemetente = Correios.Fac.limpaCep(mskCepRemetente.Text);
                                var numRemetente = Convert.ToInt32(npNumRemetente.Value);
                                var digControle = int.Parse(Correios.Fac.CepNet(cepDestino, 1));
                                var dataPostagem = int.Parse(mcalendarDtPostagem.SelectionStart.ToString("ddMMyy"));
                                string seqFormatada_11 = string.Format("{0:00000000000}", seq);

                                string Peso = string.Format("{0:000000}", Convert.ToInt16(npPeso.Value));

                                //Para utilizar no datamatrix
                                var cif = Correios.Fac.Cif(int.Parse(mskDR.Text), int.Parse(mskCodAdm.Text), Convert.ToInt32(npLote.Value),
                                                      seq, dataPostagem, ls.Keys[i].Substring(0, 8), false);

                                //Para exibição
                                var cif_Formatado = Correios.Fac.Cif(int.Parse(mskDR.Text), int.Parse(mskCodAdm.Text), Convert.ToInt32(npLote.Value),
                                                     seq, dataPostagem, ls.Keys[i].Substring(0, 8), true);

                                var cif_128 = Barcode.TrocaCaracter_128c(cif);

                                var dataMatrix = Correios.Fac.DataMatrix(cepDestino, numDestino, cepRemetente, numRemetente,
                                                                         digControle, 1, cif, 0, int.Parse(servico), 0, int.Parse(mskCNAE.Text));

                                #region Direcao de CEP
                                C5.KeyValuePair<string, string> keyValuePair;

                                var busca = direcaoCep.TrySuccessor($"{cepDestino}", out keyValuePair);

                                //Caso for fixo, então substitui o delimitador ; por tab, caso substitui por um espaço
                                if (busca)
                                    dirCep = tipo == 0 ? Regex.Replace($"{keyValuePair.Value}", ";", "\t") :
                                        Regex.Replace($"{keyValuePair.Value}", ";", $"");
                                else
                                    dirCep = "";
                                #endregion

                                //midia
                                stwMidia.WriteLine($"2{seqFormatada_11}{Peso}{cepNet.Substring(0, 8)}{servico}");

                                if (tipo == 0)
                                    stw.WriteLine($"{ls.Values[i]}\t{dataMatrix}\t{dirCep}\t{cif}\t{cif_Formatado}\t{dataPostagem}\t{cepNet}\t{seqFormatada_11}");
                                else
                                 if (tipo == 1)
                                    stw.WriteLine($"{ls.Values[i]}{dataMatrix}{dirCep}{cif}{cif_Formatado}{dataPostagem}{cepNet}{seqFormatada_11}");
                                else
                                if (tipo == 2)
                                {
                                    var split = Regex.Split(ls.Values[i], @"\r?\n");

                                    stw.WriteLine($"{split[0]}{dataMatrix}{dirCep}{cif}{cif_Formatado}{dataPostagem}{cepNet}{seqFormatada_11}");

                                    for (int j = 1; j < split.Length; j++)
                                        if (!(Regex.IsMatch(split[j], "^$")))
                                            stw.WriteLine(split[j]);
                                }

                            }
                            else
                            {
                                stwInvalido.WriteLine(ls.Values[i]);
                                cepErrados++;
                            }
                        }

                        string seqFormatada_7 = string.Format("{0:0000000}", seq);
                        string pesoTotal = string.Format("{0:0000000000}", (seq * Convert.ToInt16(npPeso.Value)));
                        stwMidia.WriteLine($"4{seqFormatada_7}{pesoTotal}");

                        total = seq;
                    }
                }
            }
        }

        private string definindoNomeArquivo(string complementoNome)
        {
            return $"{fileInfo.DirectoryName}\\{fileInfo.Name.Replace(".txt", "").Replace(".TXT", "")}_{complementoNome}.txt";
        }

        private string definindoNomeArquivoMidia()
        {
            string lote =  string.Format("{0:00000}",Convert.ToInt16(npLote.Value));
            return $"{fileInfo.DirectoryName}\\{mskCodAdm.Text}_{lote}_UNICA_{mskDR.Text}.txt";
        }

        #region Limpando Memoria
        private void limparMemoria()
        {
            limparDefinicoes();

            if (fileInfo != null)
                fileInfo = null;

            if (ls != null)
                ls = null;

            //limpando rastros do arquivo
            opfdArq.FileName = String.Empty;

            GC.Collect();
        }

        private void limparDefinicoes()
        {
            grMulti.Enabled = false;
            grFixo.Enabled = false;
            grDelimitado.Enabled = false;

            txBoxArquivo.Clear();
            cboxTipo.ResetText();
            tipo = -1;
            cbHeader.Checked = false;
            cbTrailler.Checked = false;

            cbDelimitador.ResetText();
            npDelCEPDestino.Value = 0;
            npDelCompCepDestino.Value = 0;
            npDelNumEndereco.Value = 0;

            npFixoCepDestino.Value = 0;
            npFixoTamanho.Value = 8;
            npFixoNumEnd.Value = 0;
            npFixoTamanhoNumEnd.Value = 0;

            npMultiPosChave.Value = 1;
            npMultiCepDest.Value = 1;
            npMultiTamanhoCepDest.Value = 8;
            npMultiNumEnd.Value = 0;
            npMultiTamanhoEnd.Value = 0;
            txBoxMultiChave.Clear();

            npLote.Value = 1;
            npPeso.Value = 480;

            mcalendarDtPostagem.SelectionStart = DateTime.Now;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tipo = cboxTipo.SelectedIndex;

            grDelimitado.Enabled = false;
            grFixo.Enabled = false;
            grMulti.Enabled = false;

            if (tipo == 0)
                grDelimitado.Enabled = true;
            else
            if (tipo == 1)
                grFixo.Enabled = true;
            else
                if (tipo == 2)
                grMulti.Enabled = true;
        }

        private void cbDelimitador_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDelimitador.SelectedIndex == 0)
                del = ';';
            else
            if (cbDelimitador.SelectedIndex == 1)
                del = '|';
            else
            if (cbDelimitador.SelectedIndex == 2)
                del = '#';
            else
            if (cbDelimitador.SelectedIndex == 3)
                del = '\t';
        }

        #endregion

        #region Dados para o usuario
        private void exibirDetalhes()
        {

            lblLocal.Text = local.ToString();
            lblEstadual.Text = estadual.ToString();
            lblNacional.Text = nacional.ToString();
            lblErrados.Text = cepErrados.ToString();
            lbtotal2.Text = total.ToString();
        }
        #endregion

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            limparMemoria();
            limpaEstatisticas();
            exibirDetalhes();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (verificaFileInfo())
            {
                try
                {
                    Work();

                    MessageBox.Show("Midia e Arquivo DataMatrix2D gerados com sucesso.", "Concluído!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    limparMemoria();

                }
                
            }
            else
                MessageBox.Show("É necessário selecionar um arquivo.", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private bool verificaFileInfo()
        {
            return fileInfo != null ? true : false; 
        }
    }
}
