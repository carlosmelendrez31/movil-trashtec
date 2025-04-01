using System.Text;
using System.Threading.Tasks;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.Exceptions;
using System.Linq;

namespace Trash_TecMovil.Services
{
    public class BluetoothService
    {
        private readonly IAdapter _adapter;
        private IDevice? _device;

        public BluetoothService()
        {
            _adapter = CrossBluetoothLE.Current.Adapter;
        }

        public async Task<string?> ObtenerLlenadoAsync()
        {
            try
            {
                // Escanear dispositivos Bluetooth cercanos
                _adapter.ScanTimeout = 5000;
                await _adapter.StartScanningForDevicesAsync();

                _device = _adapter.ConnectedDevices.FirstOrDefault(d => d.Name == "BoteESP32");

                if (_device == null)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "No se encontró el bote.", "OK");
                    return null;
                }

                await _adapter.ConnectToDeviceAsync(_device);
                var service = (await _device.GetServicesAsync()).FirstOrDefault();
                if (service == null) return null;

                var characteristic = (await service.GetCharacteristicsAsync()).FirstOrDefault();
                if (characteristic == null) return null;

                // Leer los datos correctamente
                var (bytes, _) = await characteristic.ReadAsync();
                return Encoding.UTF8.GetString(bytes);
            }
            catch (DeviceConnectionException)
            {
                await App.Current.MainPage.DisplayAlert("Error", "No se pudo conectar al bote.", "OK");
                return null;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"Bluetooth error: {ex.Message}", "OK");
                return null;
            }
        }
    }
}

