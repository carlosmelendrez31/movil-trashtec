using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;

public class BluetoothService
{
    private readonly IAdapter _adapter;
    private IDevice _device;
    private ICharacteristic _characteristic;

    public BluetoothService()
    {
        _adapter = CrossBluetoothLE.Current.Adapter;
    }

    public async Task<bool> ConectarAsync(string nombreDispositivo)
    {
        try
        {
            // 🔎 Escanea dispositivos Bluetooth
            _adapter.ScanTimeout = 5000; // 5 segundos de escaneo
            await _adapter.StartScanningForDevicesAsync();

            // 🔍 Busca el dispositivo por nombre
            _device = _adapter.DiscoveredDevices.FirstOrDefault(d => d.Name == nombreDispositivo);

            if (_device == null)
            {
                Console.WriteLine("❌ Dispositivo no encontrado.");
                return false;
            }

            // 🔗 Conectar al dispositivo
            await _adapter.ConnectToDeviceAsync(_device);

            // 📡 Obtener servicios y características
            var services = await _device.GetServicesAsync();
            var service = services.FirstOrDefault();

            if (service == null)
            {
                Console.WriteLine("❌ No se encontraron servicios.");
                return false;
            }

            _characteristic = (await service.GetCharacteristicsAsync()).FirstOrDefault();
            return _characteristic != null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error de conexión: {ex.Message}");
            return false;
        }
    }

    public async Task<string?> ObtenerLlenadoAsync()
    {
        if (_characteristic == null) return null;

        try
        {
            var (bytes, resultCode) = await _characteristic.ReadAsync(); // 👈 Extrae los valores correctamente

            return bytes != null && bytes.Length > 0 ? Encoding.UTF8.GetString(bytes) : null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error al leer datos: {ex.Message}");
            return null;
        }
    }
}

