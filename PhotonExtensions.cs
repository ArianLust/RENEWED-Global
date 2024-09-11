using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

using Photon.Pun;
using Photon.Realtime;

public static class PhotonExtensions {

    // Dictionary using UserId as the key and a comment yum yum skibidis
    private static readonly Dictionary<string, string> SPECIAL_PLAYERS = new() {
        ["cf03abdb5d2ef1b6f0d30ae40303936f9ab22f387f8a1072e2849c8292470af1"] = "ipodtouch0218",
        ["d5ba21667a5da00967cc5ebd64c0d648e554fb671637adb3d22a688157d39bf6"] = "mindnomad",
        ["95962949aacdbb42a6123732dabe9c7200ded59d7eeb39c889067bafeebecc72"] = "MPS64",
        ["7e9c6f2eaf0ce11098c8a90fcd9d48b13017667e33d09d0cc5dfe924f3ead6c1"] = "Fawndue",
        ["7677b1c69e1b86d5347826c5cac6b7f8b0b6d52bb9f73c7e910a15ee4294e7c8"] = "Lust on da Unity Edidor"
    };

    public static bool IsMineOrLocal(this PhotonView view) {
        return !view || view.IsMine;
    }

    public static bool HasRainbowName(this Player player) {
        if (player == null || player.UserId == null)
            return false;

        // Compute the SHA256 hash of the UserId
        byte[] bytes = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(player.UserId));
        StringBuilder sb = new();
        foreach (byte b in bytes)
            sb.Append(b.ToString("X2"));

        // Convert hash to a lowercase string
        string hash = sb.ToString().ToLower();

        // Check if the hashed UserId is in the SPECIAL_PLAYERS dictionary
        return SPECIAL_PLAYERS.ContainsKey(hash);
    }

    // Optional: Get the comment associated with the UserId
    public static string GetPlayerComment(this Player player) {
        if (player == null || player.UserId == null)
            return null;

        // Compute the SHA256 hash of the UserId
        byte[] bytes = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(player.UserId));
        StringBuilder sb = new();
        foreach (byte b in bytes)
            sb.Append(b.ToString("X2"));

        // Convert hash to a lowercase string
        string hash = sb.ToString().ToLower();

        // Return the comment if the hash exists in the dictionary, otherwise return null
        return SPECIAL_PLAYERS.TryGetValue(hash, out string comment) ? comment : null;
    }

    // Uncomment if you want to implement custom RPC functions
    // public static void RPCFunc(this PhotonView view, Delegate action, RpcTarget target, params object[] parameters) {
    //     view.RPC(nameof(action), target, parameters);
    // }
}
