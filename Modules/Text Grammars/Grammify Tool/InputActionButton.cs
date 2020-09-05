using Chinchillada.Foundation;
using Rewired;
using UnityEngine;
using UnityEngine.UI;

namespace Mutiny.Thesis.Tools
{
    public class InputActionButton : ChinchilladaBehaviour
    {
        [SerializeField] private string inputAction;

        [SerializeField] private int playerIndex = 0;

        [SerializeField] private Button button;

        private Player player;
        
        private void Execute() => this.button.OnSubmit(null);

        private void Start() => this.player = ReInput.players.GetPlayer(this.playerIndex);

        private void Update()
        {
            if (this.player.GetButtonDown(this.inputAction))
                this.Execute();
        }

    }
}