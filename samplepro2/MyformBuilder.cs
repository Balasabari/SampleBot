using Microsoft.Bot.Builder.FormFlow;
using System;
using Microsoft.Bot.Builder.Dialogs;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace samplepro2
{

    public  enum Bronds
    {
        BMW,Volvo

    }
    [Serializable]
    
    public class MyformBuilder
    {      

        [Prompt("Would like to buy car?{||}")]
        public bool NewCar { get; set; }
        public Bronds? Brand { get; set; }

       
        public static IForm<MyformBuilder> BuildForm()
        {
            return new FormBuilder<MyformBuilder>()
            .Build();
        }

        
    }
}