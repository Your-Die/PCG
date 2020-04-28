using Chinchillada.Utilities;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Mutiny
 {
     public class TextPresenter : GeneratorPresenter<string>
     {
         [SerializeField, Required, FindComponent(SearchStrategy.InChildren)]
         private TMP_Text text;

         public override void Present(string content)
         {
             this.text.text = content;
         }
     }
 }