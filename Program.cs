using System;
using System.Collections.Generic;
using System.Media;
class GeniusGame
{
   static List<int> sequence = new List<int>();
   static Random random = new Random();
   static int currentLevel = 1;
   static bool gameOver = false;
   static void Main()
   {
       Console.WriteLine("Bem-vindo ao Genius!");
       while (!gameOver)
       {
           PlayRound();
       }
       Console.WriteLine("Fim de jogo! Pressione qualquer tecla para sair.");
       Console.ReadKey();
   }
   static void PlayRound()
   {
       Console.WriteLine($"\nNível {currentLevel} - Repita a sequência:");
       GenerateSequence();
       ShowSequence();
       GetPlayerInput();
   }
   static void GenerateSequence()
   {
       sequence.Clear(); // Limpar a sequência anterior
       for (int i = 0; i < currentLevel + 7; i++)
       {
           sequence.Add(random.Next(1, 5)); // 1: Vermelho, 2: Verde, 3: Azul, 4: Amarelo
       }
   }
   static void ShowSequence()
   {
       foreach (int color in sequence)
       {
           Console.WriteLine($"Botão {color}");
           System.Threading.Thread.Sleep(1000); // Delay para mostrar a sequência
           Console.Clear(); // Limpar a tela entre os botões
       }
   }
   static void GetPlayerInput()
   {
       List<int> playerSequence = new List<int>();
       for (int i = 0; i < sequence.Count; i++)
       {
           Console.Write("Digite o número do botão: ");
           if (!int.TryParse(Console.ReadLine(), out int input) || input < 1 || input > 4)
           {
               Console.WriteLine("Entrada inválida. O número do botão deve ser de 1 a 4.");
               i--;
               continue;
           }
           playerSequence.Add(input);
       }
       CheckPlayerInput(playerSequence);
   }
   static void CheckPlayerInput(List<int> playerSequence)
   {
       for (int i = 0; i < sequence.Count; i++)
       {
           if (playerSequence[i] != sequence[i])
           {
               Console.WriteLine("Você errou! Fim de jogo.");
               PlaySound("defeat.wav");
               gameOver = true;
               return;
           }
       }
       Console.WriteLine("Sequência correta! Próximo nível.");
       PlaySound("victory.wav");
       if (currentLevel == 4)
       {
           Console.WriteLine("Parabéns! Você venceu na dificuldade 4.");
           PlaySound("ultimate_victory.wav");
           gameOver = true;
       }
       else
       {
           currentLevel++;
       }
   }
   static void PlaySound(string soundFile)
   {
       try
       {
           SoundPlayer player = new SoundPlayer(soundFile);
           player.Play();
       }
       catch (Exception ex)
       {
           Console.WriteLine($"Erro ao reproduzir som: {ex.Message}");
       }
   }
}
