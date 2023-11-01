using AppAgenda.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppAgenda
{
    public partial class MainPage : ContentPage
    {

        bool carregando;

        public MainPage()
        {
            InitializeComponent();
        }

        public void TelaCarregamento() 
        {
            if(carregando) 
            {
                BVTelaPreta.IsVisible = false;
                ACTRoda.IsVisible = false;
                ACTRoda.IsRunning = false;

                carregando= false;
            }
            else
            {
                BVTelaPreta.IsVisible = true;
                ACTRoda.IsVisible = true;
                ACTRoda.IsRunning = true;

                carregando = true;
            }
        }

        private async void BTNLogar_Clicked(object sender, EventArgs e)
        {
            try
            {
                TelaCarregamento();

                var logar = new ServicosUser();
                bool verificarlogin = await logar.VerificarLogin(TXTEmail.Text, TXTSenha.Text);

                if(verificarlogin) 
                {
                    await DisplayAlert("Sucesso", "Usuário logado", "Ok");
                    Navigation.PushAsync(new Contatos());

                    TXTEmail.Text = string.Empty; 
                    TXTSenha.Text = string.Empty;   

                    TelaCarregamento();
                }
                else
                {
                    await DisplayAlert("Falha", "Usuario e senha não correspondem", "Ok");
                    TelaCarregamento();
                }

            }
            catch
            {
                await DisplayAlert("Falha", "Ocorreu um erro", "Ok");
                TelaCarregamento();
            }
        }

        private void BTNCriar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CriarAcesso());
        }

        private void BTNSobre_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Sobre());
        }
    }
}
