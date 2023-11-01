﻿using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAgenda.Classes
{
    public class ServicosUser
    {
        public static string SenhaFirebase = "YuIKyXWMtXIdJvquCR9txUF7S5tmh66OnEp1XU9U";
        FirebaseClient Cliente = new FirebaseClient("https://agendaapp-68d57-default-rtdb.firebaseio.com/", new FirebaseOptions { AuthTokenAsyncFactory = () => Task.FromResult(SenhaFirebase)});

        public async Task<bool> RegistrarUsuario(string user, string senha)
        {
            try
            {
                await Cliente.Child("Usuarios")
                    .PostAsync(new Usuarios()
                    {
                        usuario = user, 
                        senha = senha
                    });
                return true;
            }
            catch
            { 
                return false;
            }
        }

        public async Task<bool> VerificarLogin(string login, string loginsenha)
        {
            var consultar = (await Cliente.Child("Usuarios")
                .OnceAsync<Usuarios>())
                .Where(u => u.Object.usuario == login)
                .Where(u => u.Object.senha == loginsenha)
                .FirstOrDefault();

            return consultar != null;
        }

        public async Task<bool> VerificarDuplicado(string email)
        {
            var consultar = (await Cliente.Child("Usuarios")
                .OnceAsync<Usuarios>())
                .Where(u => u.Object.usuario == email)
                .FirstOrDefault();

            return consultar != null;   
        }



    }
}
