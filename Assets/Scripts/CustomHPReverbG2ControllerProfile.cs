using UnityEngine.XR.OpenXR.Features.Interactions;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;

[UnityEngine.Scripting.Preserve]
public class CustomHPReverbG2ControllerProfile : HPReverbG2ControllerProfile
{
    private const string kDeviceProductName = "HP Reverb G2 Controller";

    protected override void RegisterDeviceLayout()
    {
        InputSystem.RegisterLayout<XRController>(
            matches: new InputDeviceMatcher()
                .WithProduct(kDeviceProductName));
    }

    protected override void UnregisterDeviceLayout()
    {
        InputSystem.RemoveLayout(kDeviceProductName);
    }

    protected override string GetDeviceLayoutName()
    {
        return "HPReverbG2Controller";
    }
}
