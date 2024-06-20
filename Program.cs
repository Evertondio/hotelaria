using System.Globalization;
using controller;
using models;

//declaração de variaveis
ReservaController controller = new ReservaController();
bool consicaoHospede;
string opcao;
bool consulta;
int opcaoSuite;
bool condicaoSuite;
//fim de declaração de variaveis

//metodo menu para ser chamado mais de uma vez
void menu()
{
    Console.WriteLine("Hotel Dio Resort");
    Console.WriteLine("\r\n");
    Console.WriteLine("1-Registrar Reserva");
    Console.WriteLine("2-Imprimir Reserva");
    Console.WriteLine("3-Qualquer tecla para sair");
    opcao = Console.ReadLine();
 consulta = opcao == "1" || opcao == "2";
}
//cadatrar varios hospedes
void CadastrarHospedes()
{
    
List<PessoaModel> lista = new List<PessoaModel>();
    string opcaoHospede = "sim";
    consicaoHospede = opcaoHospede == "sim";
    while(consicaoHospede)
    {  
      PessoaModel hospede = new PessoaModel();
      Console.Clear();
      Console.WriteLine("Registrando Reserva");
      Console.WriteLine("Favor informar primeiro nome do hospede: ");
      hospede.Nome = Console.ReadLine();
      Console.WriteLine("Favor informar sobrenome do hospede: ");
      hospede.Sobrenome = Console.ReadLine();
      lista.Add(hospede);
      Console.WriteLine("Deseja adicionar mais algum hospede na reserva? S para sim ou qualquer tecla para sair");
      opcaoHospede = Console.ReadLine().ToString().ToLower();
      consicaoHospede = opcaoHospede == "sim" || opcaoHospede == "s";
    }
    controller.CadastrarHospedes(lista);
    Console.Clear();
}
//tipo de suite
void menuSuite()
{
    Console.WriteLine("Favor Escolher a suite abaixo:");
    Console.WriteLine("1-Master valor de R$ 150,00 á diaria");
    Console.WriteLine("2-Stander valor de R$ 350,00 á diaria");
    Console.WriteLine("3-Presidencial valor de R$ 650,00 á diaria");
    opcaoSuite = Convert.ToInt32(Console.ReadLine());
     condicaoSuite = opcaoSuite > 0 && opcaoSuite <= 3;
}
//metodo de cadastro de suite
void cadastrarSuite() 
{
    SuiteModel suite = new SuiteModel();
    menuSuite();
     
    while(condicaoSuite)
    {
        switch(opcaoSuite) 
        {
          case 1:
                if(controller.ObserQuantidadeHospedes()>2)
                {
                    Console.WriteLine("Desculpe! mas infelizmente a suite master, só comporta até duas pessoas!");
                    Console.WriteLine("Favor digite qualquer tecla para continuar");
                    Console.ReadLine();
                    Console.Clear();
                    menuSuite();
                     
                }
                else 
                {
                   suite.Capacidade = 2;
                    suite.TipoSuite = "Master";
                    suite.ValorDiaria = Convert.ToDecimal("150");
                    controller.CadastrarSuites(suite);
                    return;
                }
                break;  
        case 2:
                if (controller.ObserQuantidadeHospedes()>4)
                {
                    Console.WriteLine("Desculpe! mas infelizmente a suite Stander só comporta até 4 pessoas");
                    Console.WriteLine("Favor digite qualquer tecla para continuar");
                    Console.ReadLine();
                    Console.Clear();
                    menuSuite();
                }
                else 
                {
                    suite.Capacidade = 4;
                    suite.ValorDiaria = Convert.ToDecimal("350");
                    suite.TipoSuite = "Stander";
                    controller.CadastrarSuites(suite);
                    return;
                }
                    break;
         case 3: 
                 if (controller.ObserQuantidadeHospedes()>8) 
                 {
                    Console.WriteLine("Desculpe! mas infelizmente a suite Presidencial só comporta até 8 pessoas");
                    Console.WriteLine("Favor digite qualquer tecla para continuar");
                    Console.ReadLine();
                    Console.Clear();
                    menuSuite();
                 }
                 else {
                    suite.Capacidade = 8;
                    suite.ValorDiaria = Convert.ToDecimal("650");
                    suite.TipoSuite = "Presidencial";
                    controller.CadastrarSuites(suite);                
                    return;
                 }
                break;             
          default:
                Console.WriteLine("Por favor é obrigatório escolher algumas das opcoes acima");
                opcaoSuite = Convert.ToInt32(Console.ReadLine());
                break;  
        }
        condicaoSuite = opcaoSuite > 0 && opcaoSuite <= 3;
    } 
    
}
//calcular desconto
void registrarCheckinDias()
{
    Console.WriteLine("Favor informar total de dias para reserva");
    int dias = Convert.ToInt32(Console.ReadLine());
    controller.RegisterDesconto(dias);
    Console.Clear();
    menu();
}
//aqui nesse metodo podemos cadastrar mais de uma reseva e em 
//cada reserva tem sua suite e hospedes
void cadastrarReserva() 
{
    
    CadastrarHospedes();
    cadastrarSuite();
    registrarCheckinDias();
    controller.RegistrarReserva();

}
//aqui vai imprimir a reserva cadastrada pelo nome do hospede
//por que existem pode acontecer de cadastrar varias reservas
void imprimirReserva()
{
    ReservaModel reservaTemp = new ReservaModel();
    Console.WriteLine("Detalhe da Reserva");
    Console.WriteLine("\r\n");
    Console.WriteLine("Favor informe o nome de um hospede");
    string nome = Console.ReadLine();
    reservaTemp = controller.imprimirReseva(nome);
    if(reservaTemp.id>0)
    {
        Console.WriteLine("Hospedes Cadastrados");
    foreach(PessoaModel p in reservaTemp.hospedes)
    {
        Console.WriteLine($"Hospede: {p.Nome} {p.Sobrenome}");
    }
    Console.WriteLine("\r\n");
    Console.WriteLine("Detalhes ta suite");
    Console.WriteLine($"Suite Reservada: {reservaTemp.suite.TipoSuite}");
    Console.WriteLine($"Capacidade maxima: {reservaTemp.suite.Capacidade}");
    string formatValorDiaria = reservaTemp.suite.ValorDiaria.ToString("c2");
    Console.WriteLine($"Valor da diária: {formatValorDiaria}");
    Console.WriteLine("**********************");
    string desconto = reservaTemp.diasReservados >= 10 ? "10%" : "Nao";
    Console.WriteLine($"desconto: {desconto}");
        
    Console.WriteLine($"Data checkin: { DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}");
    Console.WriteLine($"Data checkout: {DateTime.Now.AddDays(reservaTemp.diasReservados).Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}");
    decimal valorTotalReserva = controller.CalcularValorDaDiaria();
    string formatValorTotal = valorTotalReserva.ToString("c2");
    Console.WriteLine($"Valor total da Hospedagem:{formatValorTotal}");
    Console.WriteLine("\r\n");
    Console.WriteLine("Obrigado pela estadia");
    Console.ReadLine();
    }
    else
    {
        Console.WriteLine("Desculpe mais nao foi encontrada nenhuma reserva!");
        Console.ReadLine();
    }
    
    Console.Clear();
    menu();
}



//inicio sistema

menu();
 
while (consulta) 
{
   switch(opcao) 
   {
     case "1":
            cadastrarReserva();   
            break;
     case "2":
            imprimirReserva();
            break;  
     default:
            Console.WriteLine("Obrigado pelo contato até mais");
            break;       
   }
}
//fim do sistema
