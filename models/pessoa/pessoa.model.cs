using System;
using System.Linq;
namespace models {
  class PessoaModel {
    private string nome;
    private string sobrenome;

    public string Nome {
      get {
        return nome;
      }
      set {
        nome = value;
      }
    }
    public string Sobrenome {
      get {
        return sobrenome;
      }
      set {
        sobrenome = value;
      }
    }
  }
}