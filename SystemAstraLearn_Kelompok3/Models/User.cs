using System;
using System.Collections.Generic;

namespace SystemAstraLearn_Kelompok3.Models;

public partial class User
{
    public int IdPengguna { get; set; }

    public string NamaLengkap { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string HakAkses { get; set; } = null!;
}
