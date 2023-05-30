using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public ToggleGroup themeGroup;
    public ToggleGroup modeGroup;

    public Text lastResultText;
    public Text bestResultText;

    private Config _config = new Config();

    // Start is called before the first frame update
    void Start()
    {
        var last = GameManager.Instance.GetLastResult();
        var best = GameManager.Instance.GetBestResult();
        lastResultText.text = $"上次：分数 {last.score}，长度 {last.length}";
        bestResultText.text = $"最佳：分数 {best.score}，长度 {best.length}";

        _config = GameManager.Instance.GetLastConfig();

        themeGroup.SetAllTogglesOff();
        var themeToggles = themeGroup.GetComponentsInChildren<Toggle>();
        foreach (var toggle in themeToggles)
        {
            var theme = VarUtils.GetVar<Options.Theme>(toggle, "Theme");
            if (theme != null && _config.theme == theme)
            {
                toggle.isOn = true;
                break;
            }
        }
        modeGroup.SetAllTogglesOff();
        var modeToggles = modeGroup.GetComponentsInChildren<Toggle>();
        foreach (var toggle in modeToggles)
        {
            var mode = VarUtils.GetVar<Options.Mode>(toggle, "Mode");
            if (mode != null && _config.mode == mode)
            {
                toggle.isOn = true;
                break;
            }
        }
    }

    public void StartGame()
    {
        var themeToggle = themeGroup.ActiveToggles().FirstOrDefault();
        if (themeToggle != null)
        {
            Options.Theme? theme = VarUtils.GetVar<Options.Theme>(themeToggle, "Theme");
            if (theme.HasValue)
            {
                _config.theme = theme.Value;
            }
        }

        var modeToggle = modeGroup.ActiveToggles().FirstOrDefault();
        if (modeToggle != null)
        {
            Options.Mode? mode = VarUtils.GetVar<Options.Mode>(modeToggle, "Mode");
            if (mode.HasValue)
            {
                _config.mode = mode.Value;
            }
        }

        GameManager.Instance.StartGame(_config);
    }
}