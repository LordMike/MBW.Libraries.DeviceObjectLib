// ReSharper disable InconsistentNaming
namespace DeviceObjectLib
{
    internal static class StaticStrings
    {
        public const string DeviceRoot = @"\Device";

        public const string ALPCPort = "ALPC Port";
        public const string Adapter = "Adapter";
        public const string Callback = "Callback";
        public const string Composition = "Composition";
        public const string Controller = "Controller";
        public const string CoreMessaging = "CoreMessaging";
        public const string DebugObject = "DebugObject";
        public const string Desktop = "Desktop";
        public const string Device = "Device";
        public const string Directory = "Directory";
        public const string DmaAdapter = "DmaAdapter";
        public const string DmaDomain = "DmaDomain";
        public const string Driver = "Driver";
        public const string DxgkSharedResource = "DxgkSharedResource";
        public const string DxgkSharedSwapChainObject = "DxgkSharedSwapChainObject";
        public const string DxgkSharedSyncObject = "DxgkSharedSyncObject";
        public const string EtwConsumer = "EtwConsumer";
        public const string EtwRegistration = "EtwRegistration";
        public const string Event = "Event";
        public const string File = "File";
        public const string FilterCommunicationPort = "FilterCommunicationPort";
        public const string FilterConnectionPort = "FilterConnectionPort";
        public const string IRTimer = "IRTimer";
        public const string IoCompletion = "IoCompletion";
        public const string IoCompletionReserve = "IoCompletionReserve";
        public const string Job = "Job";
        public const string Key = "Key";
        public const string KeyedEvent = "KeyedEvent";
        public const string Mutant = "Mutant";
        public const string NdisCmState = "NdisCmState";
        public const string Partition = "Partition";
        public const string PcwObject = "PcwObject";
        public const string PowerRequest = "PowerRequest";
        public const string Process = "Process";
        public const string Profile = "Profile";
        public const string PsSiloContextNonPaged = "PsSiloContextNonPaged";
        public const string PsSiloContextPaged = "PsSiloContextPaged";
        public const string RawInputManager = "RawInputManager";
        public const string RegistryTransaction = "RegistryTransaction";
        public const string Section = "Section";
        public const string Semaphore = "Semaphore";
        public const string Session = "Session";
        public const string SymbolicLink = "SymbolicLink";
        public const string Thread = "Thread";
        public const string Timer = "Timer";
        public const string TmEn = "TmEn";
        public const string TmRm = "TmRm";
        public const string TmTm = "TmTm";
        public const string TmTx = "TmTx";
        public const string Token = "Token";
        public const string TpWorkerFactory = "TpWorkerFactory";
        public const string Type = "Type";
        public const string UserApcReserve = "UserApcReserve";
        public const string VRegConfigurationContext = "VRegConfigurationContext";
        public const string VirtualKey = "VirtualKey";
        public const string WaitCompletionPacket = "WaitCompletionPacket";
        public const string WindowStation = "WindowStation";
        public const string WmiGuid = "WmiGuid";


        public static WellKnownType ToWellKnownType(string typeName)
        {
            switch (typeName)
            {
                case ALPCPort:
                    return WellKnownType.ALPCPort;
                case Adapter:
                    return WellKnownType.Adapter;
                case Callback:
                    return WellKnownType.Callback;
                case Composition:
                    return WellKnownType.Composition;
                case Controller:
                    return WellKnownType.Controller;
                case CoreMessaging:
                    return WellKnownType.CoreMessaging;
                case DebugObject:
                    return WellKnownType.DebugObject;
                case Desktop:
                    return WellKnownType.Desktop;
                case Device:
                    return WellKnownType.Device;
                case Directory:
                    return WellKnownType.Directory;
                case DmaAdapter:
                    return WellKnownType.DmaAdapter;
                case DmaDomain:
                    return WellKnownType.DmaDomain;
                case Driver:
                    return WellKnownType.Driver;
                case DxgkSharedResource:
                    return WellKnownType.DxgkSharedResource;
                case DxgkSharedSwapChainObject:
                    return WellKnownType.DxgkSharedSwapChainObject;
                case DxgkSharedSyncObject:
                    return WellKnownType.DxgkSharedSyncObject;
                case EtwConsumer:
                    return WellKnownType.EtwConsumer;
                case EtwRegistration:
                    return WellKnownType.EtwRegistration;
                case Event:
                    return WellKnownType.Event;
                case File:
                    return WellKnownType.File;
                case FilterCommunicationPort:
                    return WellKnownType.FilterCommunicationPort;
                case FilterConnectionPort:
                    return WellKnownType.FilterConnectionPort;
                case IRTimer:
                    return WellKnownType.IRTimer;
                case IoCompletion:
                    return WellKnownType.IoCompletion;
                case IoCompletionReserve:
                    return WellKnownType.IoCompletionReserve;
                case Job:
                    return WellKnownType.Job;
                case Key:
                    return WellKnownType.Key;
                case KeyedEvent:
                    return WellKnownType.KeyedEvent;
                case Mutant:
                    return WellKnownType.Mutant;
                case NdisCmState:
                    return WellKnownType.NdisCmState;
                case Partition:
                    return WellKnownType.Partition;
                case PcwObject:
                    return WellKnownType.PcwObject;
                case PowerRequest:
                    return WellKnownType.PowerRequest;
                case Process:
                    return WellKnownType.Process;
                case Profile:
                    return WellKnownType.Profile;
                case PsSiloContextNonPaged:
                    return WellKnownType.PsSiloContextNonPaged;
                case PsSiloContextPaged:
                    return WellKnownType.PsSiloContextPaged;
                case RawInputManager:
                    return WellKnownType.RawInputManager;
                case RegistryTransaction:
                    return WellKnownType.RegistryTransaction;
                case Section:
                    return WellKnownType.Section;
                case Semaphore:
                    return WellKnownType.Semaphore;
                case Session:
                    return WellKnownType.Session;
                case SymbolicLink:
                    return WellKnownType.SymbolicLink;
                case Thread:
                    return WellKnownType.Thread;
                case Timer:
                    return WellKnownType.Timer;
                case TmEn:
                    return WellKnownType.TmEn;
                case TmRm:
                    return WellKnownType.TmRm;
                case TmTm:
                    return WellKnownType.TmTm;
                case TmTx:
                    return WellKnownType.TmTx;
                case Token:
                    return WellKnownType.Token;
                case TpWorkerFactory:
                    return WellKnownType.TpWorkerFactory;
                case Type:
                    return WellKnownType.Type;
                case UserApcReserve:
                    return WellKnownType.UserApcReserve;
                case VRegConfigurationContext:
                    return WellKnownType.VRegConfigurationContext;
                case VirtualKey:
                    return WellKnownType.VirtualKey;
                case WaitCompletionPacket:
                    return WellKnownType.WaitCompletionPacket;
                case WindowStation:
                    return WellKnownType.WindowStation;
                case WmiGuid:
                    return WellKnownType.WmiGuid;
                default:
                    return WellKnownType.Unknown;
            }
        }

        public static string ToString(WellKnownType type)
        {
            if (type == WellKnownType.Unknown)
                return null;

            return type.ToString();
        }
    }
}