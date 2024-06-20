using System;
using System.Collections.Generic;
using System.Linq;

namespace models {
  class ReservaModel {
    
    public int id { get; set; }
    public List<PessoaModel> hospedes { get; set; }
    
    public SuiteModel suite { get; set; }
    public int diasReservados;

  }
}