//using UnityEngine;
//using UnityEditor;
//using Challonge.Components;
//using System.Collections;

//[CustomEditor(typeof(TournamentComponent))]
//public class TournamentComponentEditor : BaseEditor
//{
//    public override void DrawCustomInspector()
//    {
        
//    }

//    public override void OnInspectorGUI()
//    {
//        base.DrawDefaultInspector();

//        TournamentComponent tournament = (TournamentComponent)target;

//        header("Create Tournament");

//        textField(ref tournament.tournamentName, "Tournament");
//        textField(ref tournament.tournamentURL, "Tournament URL");
//        tournament.tournamentType = (Challonge.Properties.TournamentType)EditorGUILayout.EnumPopup("Tournament Type", tournament.tournamentType);

//        header("Add Participant");

//        textField(ref tournament.participantName, "Participant Name");

//        if (GUILayout.Button("Refresh"))
//            tournament.Refresh();

//        if (GUILayout.Button("Delete Tournament"))
//            tournament.DeleteTournament();

//        if (GUILayout.Button("Update Tournament Details"))
//            tournament.UpdateTournamentDetails();

//        if (GUILayout.Button("Change Tournament"))
//            tournament.ChangeTournament();

//        if (GUILayout.Button("Get Match"))
//            tournament.GetMatch();

//        if (GUILayout.Button("Get All Matches"))
//            tournament.GetAllMatches();

//        if (GUILayout.Button("Add Participant"))
//            tournament.AddParticipant();

//        if (GUILayout.Button("Add Participants"))
//            tournament.AddParticipants();

//        if (GUILayout.Button("Get Participant"))
//            tournament.GetParticipant();

//        if (GUILayout.Button("Get All Participants"))
//            tournament.GetAllParticipants();

//        if (GUILayout.Button("Clear All Participants"))
//            tournament.ClearAllParticipants();

//        if (GUILayout.Button("Randomize Participant Seeding"))
//            tournament.RandomizeParticipantSeeding();

//    }
//}
