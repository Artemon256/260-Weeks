using System;
using System.Collections.Generic;
using _260Weeks.GameLogic;

namespace _260Weeks
{
    class ConsoleInterface : IPlayerInterface
    {
        private Core core;

        public ConsoleInterface()
        {
            core = new Core(this);
            core.Init(8, 4);
        }

        private void commandShowOpinions(string[] parts)
        {
            string help = "Usage: show opinions <about|of> <id|name>";
            if (parts.Length < 4)
            {
                Console.WriteLine(help);
                return;
            }

            uint id = 0;
            Member target;
            if (uint.TryParse(parts[3], out id))
                target = core.GetMemberById(id);
            else
                target = core.GetMemberByName(parts[3]);

            if (target == null)
            {
                Console.WriteLine(help);
                return;
            }

            Console.WriteLine($"{target.Name} ({target.GetType().Name} / {target.ID})");
            Console.WriteLine("===");
            switch (parts[2])
            {
                case "of":
                    if (target is President || target is MassMediaUnit)
                    {
                        Console.WriteLine("Not applicable");
                        break;
                    }
                    foreach (KeyValuePair<Member, double> entry in target.Opinions)
                        Console.WriteLine($"{entry.Key.Name} ({entry.Key.GetType().Name} / {entry.Key.ID}): {entry.Value}");
                    break;
                case "about":
                    foreach (Member member in core.Members)
                    {
                        double opinion = 0;
                        if (!member.Opinions.TryGetValue(target, out opinion))
                            continue;
                        Console.WriteLine($"{member.Name} ({member.GetType().Name} / {member.ID}): {opinion}");
                    }
                    break;
                default:
                    Console.WriteLine(help);
                    break;
            }
        }

        private void commandShowMembers()
        {
            foreach (Member member in Core.getInstance().Members)
                if (member is SocialGroup)
                {
                    string eligibility = (member as SocialGroup).VoteEligible ? "Eligible" : "Not eligible";
                    Console.WriteLine($"{member.Name} ({member.GetType().Name} / {member.ID}) - Population: {(member as SocialGroup).Population}, {eligibility}");
                }
                else if (member is MassMediaUnit)
                    Console.WriteLine($"{member.Name} ({member.GetType().Name} / {member.ID}) - Owner: {(member as MassMediaUnit).Owner.Name} ({(member as MassMediaUnit).Owner.ID})");
                else
                    Console.WriteLine($"{member.Name} ({member.GetType().Name} / {member.ID})");
        }

        private void commandShowCampaigns()
        {
            foreach (MassMediaUnit media in core.MassMedia)
            {
                Console.WriteLine($"{media.Name} ({media.GetType().Name} / {media.ID})");
                Console.WriteLine("===");
                foreach (MassMediaUnit.Campaign campaign in media.GetCampaigns())
                    Console.WriteLine($"{campaign.Subject.Name} ({campaign.Subject.GetType().Name} / {campaign.Subject.ID}) - Delta: {campaign.Delta}, Turns left: {campaign.TurnsLeft}");
                Console.WriteLine();
            }
        }

        private void commandActCampaign(string[] parts)
        {
            string help = "Usage: act campaign <id of mass media|name of mass media> <pro|against> <id of subject|name of subject> <duration>";
            if (parts.Length < 6)
            {
                Console.WriteLine(help);
                return;
            }

            uint id = 0;
            Member member;
            if (uint.TryParse(parts[2], out id))
                member = core.GetMemberById(id);
            else
                member = core.GetMemberByName(parts[2]);

            if (member == null || !(member is MassMediaUnit))
            {
                Console.WriteLine(help);
                return;
            }

            MassMediaUnit media = member as MassMediaUnit;

            id = 0;
            Member subject;
            if (uint.TryParse(parts[4], out id))
                subject = core.GetMemberById(id);
            else
                subject = core.GetMemberByName(parts[4]);

            uint duration = 0;

            if (!uint.TryParse(parts[5], out duration))
            {
                Console.WriteLine(help);
                return;
            }

            if (subject == null)
            {
                Console.WriteLine(help);
                return;
            }

            switch (parts[3])
            {
                case "pro":
                    switch (media.RunCampaign(core.Player, subject, MassMediaUnit.CampaignMode.Pro, duration))
                    {
                        case MassMediaUnit.CampaignRunResult.OK:
                            Console.WriteLine("Success: Campaign started");
                            break;
                        case MassMediaUnit.CampaignRunResult.RefusedOpinionSender:
                            Console.WriteLine("Fail: Owner of mass media hates you");
                            break;
                        case MassMediaUnit.CampaignRunResult.RefusedOpinionSubject:
                            Console.WriteLine("Fail: Owner of mass media has different opinion about subject");
                            break;
                        case MassMediaUnit.CampaignRunResult.RefusedServicePoints:
                            Console.WriteLine("Fail: Owner of mass media doesn't owe you");
                            break;
                        case MassMediaUnit.CampaignRunResult.RefusedTooManyCampaigns:
                            Console.WriteLine($"Fail: Mass media already have {Params.MaxCampaignsPerUnit} campaigns");
                            break;
                    }
                    break;
                case "against":
                    switch (media.RunCampaign(core.Player, subject, MassMediaUnit.CampaignMode.Against, duration))
                    {
                        case MassMediaUnit.CampaignRunResult.OK:
                            Console.WriteLine("Success: Campaign started");
                            break;
                        case MassMediaUnit.CampaignRunResult.RefusedOpinionSender:
                            Console.WriteLine("Fail: Owner of mass media hates you");
                            break;
                        case MassMediaUnit.CampaignRunResult.RefusedOpinionSubject:
                            Console.WriteLine("Fail: Owner of mass media has different opinion about subject");
                            break;
                        case MassMediaUnit.CampaignRunResult.RefusedServicePoints:
                            Console.WriteLine("Fail: Owner of mass media doesn't owe you");
                            break;
                        case MassMediaUnit.CampaignRunResult.RefusedTooManyCampaigns:
                            Console.WriteLine($"Fail: Mass media already have {Params.MaxCampaignsPerUnit} campaigns");
                            break;
                    }
                    break;
            }
        }

        private void commandAct(string[] parts)
        {
            string help =
@"Usage: act <campaign>
campaign    Run new campaign";
            if (parts.Length < 2)
            {
                Console.WriteLine(help);
                Console.ReadKey();
                return;
            }
            switch (parts[1])
            {
                case "campaign":
                    commandActCampaign(parts);
                    break;
                default:
                    Console.WriteLine(help);
                    break;
            }
            Console.ReadKey();
        }

        private void commandShow(string[] parts)
        {
            string help =
@"Usage: show <members|campaigns|opinions>
members     list of game members
campaigns   list of active campaigns
opinions    opinions of/about member";
            if (parts.Length < 2)
            {
                Console.WriteLine(help);
                Console.ReadKey();
                return;
            }
            switch (parts[1])
            {
                case "campaigns":
                    commandShowCampaigns();
                    break;
                case "members":
                    commandShowMembers();
                    break;
                case "opinions":
                    commandShowOpinions(parts);
                    break;
                default:
                    Console.WriteLine(help);
                    break;
            }
            Console.ReadKey();
        }

        public void PlayerTurn()
        {
            string help =
@"Available commands:
show    show statistics on smth
act     run action
turn    pass to next turn
exit    game over";
            bool session = true;
            while (session)
            {
                Console.Clear();
                Console.WriteLine($"Turn: {core.TurnNumber}");
                Console.WriteLine(help);
                string command = Console.ReadLine();
                string[] parts = command.Split(" ");
                if (parts.Length == 0)
                    continue;
                switch (parts[0])
                {
                    case "turn":
                        session = false;
                        break;
                    case "exit":
                        session = false;
                        core.GameOver();
                        break;
                    case "act":
                        commandAct(parts);
                        break;
                    case "show":
                        commandShow(parts);
                        break;
                }
            }
        }

        public bool Turn()
        {
            return core.Turn();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ConsoleInterface Interface = new ConsoleInterface();
            bool gameOn = true;
            while (gameOn)
                gameOn = Interface.Turn();
            Console.WriteLine("Game over");
        }
    }
}
