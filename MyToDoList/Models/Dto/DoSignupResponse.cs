using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDoList.Models.Dto;

class DoSignupResponse
{
    public string status {  get; set; }
    public string message { get; set; }
    public int id { get; set; }
    public string created_at { get; set; }
}
