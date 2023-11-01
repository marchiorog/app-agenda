using AppAgenda.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppAgenda
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CriarAcesso : ContentPage
    {
        bool carregando;

        public CriarAcesso()
        {
            InitializeComponent();
        }

        public void TelaCarregamento()
        {
            if (carregando)
            {
                BVTelaPreta.IsVisible = false;
                ACTRoda.IsVisible = false;
                ACTRoda.IsRunning = false;

                carregando = false;
            }
            else
            {
                BVTelaPreta.IsVisible = true;
                ACTRoda.IsVisible = true;
                ACTRoda.IsRunning = true;

                carregando = true;
            }
        }


        private async void BTNCriarAcesso_Clicked(object sender, EventArgs e)
        {

            TelaCarregamento();

            if(TXTCriarSenha.Text != TXTConfirmarSenha.Text)
            {
                await DisplayAlert("Falha", "As senhas não correspondem!", "Ok");

                TelaCarregamento();
                return;
            }

            var login = new ServicosUser();
            bool loginduplicado = await login.VerificarDuplicado(TXTCriarEmail.Text);

            if(loginduplicado)
            {
                await DisplayAlert("Falha", "Esse login já existe!", "Ok");

                TelaCarregamento();
                return;
            }


            try
            {
                var acesso = new ServicosUser();
                bool criaracesso = await acesso.RegistrarUsuario(TXTCriarEmail.Text, TXTCriarSenha.Text);

                if (criaracesso) 
                {
                    await DisplayAlert("Sucesso", "Usuário criado!", "Ok");
                    TelaCarregamento();
                    return;
                }
                else
                {
                    await DisplayAlert("Falha", "Não foi possível criar um usuário, tente novamente!", "Ok");
                    TelaCarregamento();
                    return;
                }
            }
            catch
            {
                await DisplayAlert("Falha", "Ocorreu um erro!", "Ok");
                TelaCarregamento();
            }
        }

        private void BTNCancelar_Clicked(object sender, EventArgs e)
        {
            TXTCriarEmail.Text = string.Empty;
            TXTConfirmarSenha.Text = string.Empty;
            TXTCriarSenha.Text = string.Empty;
        }
    }
}