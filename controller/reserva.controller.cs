

using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Schema;
using models;

namespace controller {
    //Controller criada para ficar mais organizado o codigo
    class ReservaController
    {
        //essa reserva vai ser sempre a mesma so vai mudar quando o cadastro
        //de reserva for chamado
        private ReservaModel reserva;
        //indice usado para buscar a reserva desejada esse
        //indice será usado no imprimir e no calcular valor total da reserva
        private int posicaoReservaAtual;
        //podemos armazenar mais de uma reserva
        private List<ReservaModel> reservas = new List<ReservaModel>();
               
        private void inicializarReserva()
        {
            reserva = new ReservaModel();
            reserva.hospedes = new List<PessoaModel>();
            reserva.suite = new SuiteModel();
        }       
        public ReservaController()
        {
            inicializarReserva();
        }
        public void RegistrarReserva()
        {

            reserva.id = reservas.Count() + 1;
            reservas.Add(reserva);
            inicializarReserva();
        }
        public void RegisterDesconto(int dias)
        {
            reserva.diasReservados = dias;
        }
        public ReservaModel imprimirReseva(string nomeHospede)
        {
        
           if(reservas.Count()>0)
           {
               bool exit = false;
            
           for(int i=0;i<reservas.Count();i++)
           {
                for(int b=0;b<reservas[i].hospedes.Count();b++)
                {
                    if(reservas[i].hospedes[b].Nome.Contains(nomeHospede))
                    {
                        posicaoReservaAtual= i;
                        exit = true;
                    }
                }
                if(exit)
                {
                    break;
                }
           }
            return reservas[posicaoReservaAtual];  
           }
           else{
                ReservaModel tempNaoEncontrado = new ReservaModel();
                tempNaoEncontrado.id = 0;
                return tempNaoEncontrado;
           }
        }
        public void CadastrarHospedes(List<PessoaModel> hospede)
        {
          reserva.hospedes.AddRange(hospede);
        }
        public void CadastrarSuites(SuiteModel suite)
        {
           reserva.suite=suite;
        }
        public int ObserQuantidadeHospedes() {
            return reserva.hospedes.Count();
        }
        public decimal CalcularValorDaDiaria()
        {

            decimal desconto = 1;
            decimal total = 0;
            if (reservas[posicaoReservaAtual].diasReservados >= 10)
            {
                desconto = reservas[posicaoReservaAtual].suite.ValorDiaria * 0.10m;
                total = (reservas[posicaoReservaAtual].suite.ValorDiaria-desconto) *reservas[posicaoReservaAtual].diasReservados;
            }
            else
            {
                total = reservas[posicaoReservaAtual].suite.ValorDiaria * reservas[posicaoReservaAtual].diasReservados;
            }
            
             return total;
        }
    }
}