using System;
using System.Linq;
namespace models {
  class SuiteModel {
    private string tipoSuite;
    private int capacidade;
    private decimal valorDiaria;

    // public SuiteModel(string tipoSuite, int capacidade, decimal valorDiaria) 
    // {
    //   this.capacidade = capacidade;
    //   this.tipoSuite = tipoSuite;
    //   this.valorDiaria = valorDiaria;
    // }

    public string TipoSuite {
      get {
        return tipoSuite;
      }
      set {
        tipoSuite = value;
      }
    }
    public int Capacidade {
      get {
        return capacidade;
      }
      set {
        capacidade = value;
      }
    }
    public decimal ValorDiaria {
      get {
        return valorDiaria;
      }
      set {
        valorDiaria = value;
      }
    }
  }
}