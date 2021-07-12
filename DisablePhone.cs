using GTA;
using GTA.Native;
using System;

namespace DisablePhone
{
  public class DisablePhone : Script
  {
    private const string CELLPHONE_SCRIPT = "cellphone_controller";
    private bool enabled;
    private readonly int cheatString;

    public DisablePhone()
    {
      this.enabled = this.Settings.GetValue<bool>("SETTINGS", "ENABLED_ON_STARTUP", true);
      this.cheatString = Game.GenerateHash(this.Settings.GetValue("SETTINGS", "CHEAT_STRING", "togglephone"));
      this.Interval = 0;
      this.Tick += new EventHandler(this.DisablePhone_Tick);
    }

    private void StartCellphoneScript()
    {
      Function.Call(Hash._0x6EB5F71AA68F2E8E, (InputArgument) "cellphone_controller");
      for (int index = 0; index < 100; ++index)
      {
        if (!Function.Call<bool>(Hash._0xE6CC9F3BA0FB9EF1, (InputArgument) "cellphone_controller"))
          Script.Wait(1);
        else
          break;
      }
      if (!Function.Call<bool>(Hash._0xE6CC9F3BA0FB9EF1, (InputArgument) "cellphone_controller"))
        return;
      Function.Call(Hash._0xE81651AD79516E48, (InputArgument) "cellphone_controller", (InputArgument) 1424);
      Function.Call(Hash._0xC90D2DCACD56184C, (InputArgument) "cellphone_controller");
    }

    private void DisablePhone_Tick(object sender, EventArgs e)
    {
      if (Function.Call<bool>(Hash._0x557E43C447E700A8, (InputArgument) this.cheatString))
      {
        this.enabled = !this.enabled;
        if (!this.enabled)
          this.StartCellphoneScript();
        UI.ShowSubtitle(string.Format("DisablePhone {0}", this.enabled ? (object) "enabled" : (object) "disabled"));
      }
      if (!this.enabled)
        return;
      Function.Call(Hash._0x9DC711BC69C548DF, (InputArgument) "cellphone_controller");
      Game.DisableControlThisFrame(0, Control.Phone);
    }
  }
}
