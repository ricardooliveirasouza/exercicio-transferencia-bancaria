using System;

namespace transferencia
{
    class Program
    {
        public static void SolicitaNumeroConta(ref int numero, string mensagem_complementar)
        {
            Console.Clear();
            Console.WriteLine("Qual o numero da conta {0} ?", mensagem_complementar);
            numero = int.Parse(Console.ReadLine());
        }

        public static bool ProcuraContaPorNumero(Conta[] contas, int numero, ref Conta conta)
        {
            int indice;

            for(indice = 0; indice < contas.Length; ++indice)
            {
                if(contas[indice].Numero() == numero)
                {
                    conta = contas[indice];
                }
            }
            if(conta == null)
            {
                Console.WriteLine("Conta com numero {0} não existe", numero);
                return false;
            }
            return true;
        }
        public static void MensagemFinal()
        {
            Console.WriteLine();
            Console.WriteLine("pressione qualquer tecla");
            Console.ReadKey();
        }

        public static void CriarConta(ref Conta[] contas)
        {
            int qtde;
            Conta nova;
            string titular;

            Console.Clear();
            Console.WriteLine("Entre com o nome do titular da nova conta : ");
            titular = Console.ReadLine();

            nova = new Conta(titular);
            qtde = contas.Length;
            ++qtde;
            Array.Resize(ref contas, qtde);
            contas[qtde - 1] = nova;
            Console.WriteLine("Conta criada com sucesso");
            MensagemFinal();
        }

        public static void ListarContas(Conta[] contas)
        {
            int indice;
            Console.Clear();

            Console.WriteLine("NUMERO|TITULAR");

            for(indice = 0; indice < contas.Length; ++indice)
            {
                Console.WriteLine("{0,6}|{1}", contas[indice].Numero(), contas[indice].Titular());
            }
            MensagemFinal();
        }

        public static void SaldoConta(Conta[] contas)
        {
            int numero = 0;
            Conta conta = null;

            SolicitaNumeroConta(ref numero, "");

            if(ProcuraContaPorNumero(contas, numero, ref conta))
            {
                Console.WriteLine("Conta de numero {0}, de {1} com saldo de R$ {2}", conta.Numero(), conta.Titular(), conta.SaldoComChequeEspecial());
                Console.WriteLine("Limite do cheque especial de R$ {0}", conta.LimiteChequeEspecial());
            }
            MensagemFinal();
        }

        public static void Saque(Conta[] contas)
        {
            int numero = 0;
            double valor;
            Conta conta = null;

            SolicitaNumeroConta(ref numero, "");

            if(ProcuraContaPorNumero(contas, numero, ref conta))
            {
                Console.WriteLine("Qual o valor do saque ?");
                valor = double.Parse(Console.ReadLine());
                if(conta.Saque(valor))
                {
                    Console.WriteLine("Saque de R$ {0} efetuado com sucesso da conta de {1}", 
                                        valor, conta.Titular());

                }
                else
                {
                    Console.WriteLine("Não foi possivel fazer um saque de R$ {0} da conta de {1}",
                                        valor, conta.Titular());
                }
            }
            MensagemFinal();
        }

        public static void Deposito(Conta[] contas)
        {
            int numero = 0;
            double valor;
            Conta conta = null;

            SolicitaNumeroConta(ref numero, "");

            if(ProcuraContaPorNumero(contas, numero, ref conta))
            {
                Console.WriteLine("Qual o valor do deposito ?");
                valor = double.Parse(Console.ReadLine());
                if(conta.Deposito(valor))
                {
                    Console.WriteLine("Deposito de R$ {0} efetuado com sucesso da conta de {1}", 
                                        valor, conta.Titular());

                }
                else
                {
                    Console.WriteLine("Não foi possivel fazer um deposito de R$ {0} da conta de {1}",
                                        valor, conta.Titular());
                }
            }
            MensagemFinal();
        }

        public static void Transferencia(Conta[] contas)
        {
            int numero_origem = 0;
            int numero_destino = 0;
            double valor;
            Conta conta_origem = null;
            Conta conta_destino = null;

            SolicitaNumeroConta(ref numero_origem, "de origem");

            if(ProcuraContaPorNumero(contas, numero_origem, ref conta_origem))
            {
                SolicitaNumeroConta(ref numero_destino, "de destino");

                if(ProcuraContaPorNumero(contas, numero_destino, ref conta_destino))
                {
                    Console.WriteLine("Qual o valor da transferencia ?");
                    valor = double.Parse(Console.ReadLine());
                    if(conta_origem.Transferencia(conta_destino, valor))
                    {
                        Console.WriteLine("Transferencia de R$ {0} efetuada com sucesso da conta de {1} para a conta de {2}", 
                                            valor, conta_origem.Titular(), conta_destino.Titular());

                    }
                    else
                    {
                        Console.WriteLine("Não foi possivel fazer uma transferencia de R$ {0} da conta de {1} para a conta de {2}",
                                            valor, conta_origem.Titular(), conta_destino.Titular());
                    }
                }
            }
            MensagemFinal();
        }

        public static void AdicionaChequeEspecial(Conta[] contas)
        {
            int numero = 0;
            Conta conta = null;

            SolicitaNumeroConta(ref numero, "");

            if(ProcuraContaPorNumero(contas, numero, ref conta))
            {
                if(conta.AdicionaChequeEspecial())
                {
                    Console.WriteLine("Adicionado com sucesso produto de cheque especial na conta de {0}", 
                                        conta.Titular());

                }
                else
                {
                    Console.WriteLine("Não foi possivel adicionar cheque especial na conta de {0}",
                                        conta.Titular());
                }
            }
            MensagemFinal();
        }

        public static void RemoveChequeEspecial(Conta[] contas)
        {
            int numero = 0;
            Conta conta = null;

            SolicitaNumeroConta(ref numero, "");

            if(ProcuraContaPorNumero(contas, numero, ref conta))
            {
                if(conta.RemoveChequeEspecial())
                {
                    Console.WriteLine("Removido com sucesso produto de cheque especial na conta de {0}", 
                                        conta.Titular());

                }
                else
                {
                    Console.WriteLine("Não foi possivel remover cheque especial na conta de {0}",
                                        conta.Titular());
                }
            }
            MensagemFinal();
        }

        public static void AlteraLimiteChequeEspecial(Conta[] contas)
        {
            int numero = 0;
            double valor;
            Conta conta = null;

            SolicitaNumeroConta(ref numero, "");

            if(ProcuraContaPorNumero(contas, numero, ref conta))
            {
                Console.WriteLine("Qual o valor do novo limite do cheque especial ?");
                valor = double.Parse(Console.ReadLine());
                if(conta.AtribuiValorChequeEspecial(valor))
                {
                    Console.WriteLine("Novo limite de cheque especial de R$ {0} efetuado com sucesso na conta de {1}", 
                                        valor, conta.Titular());

                }
                else
                {
                    Console.WriteLine("Não foi possivel definir um novo limite de cheque especial no valor de R$ {0} na conta de {1}",
                                        valor, conta.Titular());
                }
            }
            MensagemFinal();
        }

        static void Main(string[] args)
        {
            Conta[] contas;
            int opcao;

            contas = (Conta[])Array.CreateInstance(typeof(Conta), 0);

            do
            {
                Console.Clear();
                Console.WriteLine("1 - Criar Conta");
                Console.WriteLine("2 - Listar Contas");
                Console.WriteLine("3 - Saldo Conta");
                Console.WriteLine("4 - Saque");
                Console.WriteLine("5 - Deposito");
                Console.WriteLine("6 - Transferencia");
                Console.WriteLine("7 - Adiciona Cheque Especial");
                Console.WriteLine("8 - Remove Cheque Especial");
                Console.WriteLine("9 - Altera Limite do Cheque Especial");
                Console.WriteLine("0 - Sair");

                opcao = int.Parse(Console.ReadLine());

                switch(opcao) 
                {
                    case 1:
                        CriarConta(ref contas);
                        break;
                    case 2:
                        ListarContas(contas);
                        break;
                    case 3:
                        SaldoConta(contas);
                        break;
                    case 4:
                        Saque(contas);
                        break;
                    case 5:
                        Deposito(contas);
                        break;
                    case 6:
                        Transferencia(contas);
                        break;
                    case 7:
                        AdicionaChequeEspecial(contas);
                        break;
                    case 8:
                        RemoveChequeEspecial(contas);
                        break;
                    case 9:
                        AlteraLimiteChequeEspecial(contas);
                        break;
                }
            } while(opcao != 0);
        }
    }
}
