namespace transferencia
{
    public class Conta
    {
        private int numero {get;}
        private string titular {get; set;}
        private double saldo {get; set;}
        private bool temChequeEspecial {get; set;}
        private double chequeEspecial {get; set;}

        private static int contador = 0;

        public Conta(string titular)
        {
            this.numero = ++contador;
            this.titular = titular;
            this.saldo = 0.0;
            this.temChequeEspecial = false;
            this.chequeEspecial = 0.0;
        }

        public bool AdicionaChequeEspecial()
        {
            if(this.temChequeEspecial)
            {
                return false;
            }
            this.temChequeEspecial = true;
            return true;
        }

        public bool RemoveChequeEspecial()
        {
            if(!this.temChequeEspecial)
            {
                return false;
            }
            else if(this.saldo < 0.0)
            {
                return false;
            }
            this.chequeEspecial = 0.0;
            this.temChequeEspecial = false;;
            return true;
        }

        public bool AtribuiValorChequeEspecial(double valor)
        {
            if(!this.temChequeEspecial)
            {
                return false;
            }
            else if(this.saldo < 0.0)
            {
                // A conta não pode ficar negativa, então o novo 
                //   valor do cheque especial não pode deixar a 
                //   conta negativa.
                if(this.saldo + valor < 0.0)
                {
                    return false;
                }
            }
            this.chequeEspecial = valor;
            return true;
        }

        public string Titular()
        {
            return this.titular;
        }

        public int Numero()
        {
            return this.numero;
        }

        public double LimiteChequeEspecial()
        {
            return this.chequeEspecial;
        }

        public double SaldoSemChequeEspecial()
        {
            return this.saldo;
        }

        public double SaldoComChequeEspecial()
        {
            return this.saldo + (this.temChequeEspecial?this.chequeEspecial:0.0);
        }

        public bool TemChequeEspecial()
        {
            return this.temChequeEspecial;
        }

        public bool Deposito(double valor)
        {
            if(valor <= 0.0)
            {
                return false;
            }
            this.saldo += valor;
            return true;
        }

        public bool Saque(double valor)
        {
            if(valor <= 0.0)
            {
                return false;
            }
            if((this.saldo + this.chequeEspecial) < valor)
            {
                return false;
            }
            this.saldo -= valor;
            return true;
        }

        public bool Transferencia(Conta destino, double valor)
        {
            if(this.Saque(valor))
            {
                return destino.Deposito(valor);
            }
            return false;
        }
    }
}