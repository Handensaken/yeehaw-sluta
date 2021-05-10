using System;
using System.Reflection;
namespace SlutProject
{
    public class StartRoom : Room
    {
        public StartRoom(){
            Name = "Start Room";    //assigns the room name
        }
        public override string[] GetChoices()   //creates the relevant chices for the start room
        {
            choices.Clear();
            choices.Add("Shop");
            choices.Add("Battle");
            choices.Add("Stay");
            return base.GetChoices();
        }
        public override string[] GetActions()   //Gives specific room actions
        {
            Actions = new string[]{
                "Check Info Board",
                "Back"
            };
            return Actions;
        }
    }
}
