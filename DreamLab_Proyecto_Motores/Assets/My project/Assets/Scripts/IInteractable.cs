// IInteractable.cs
public interface IInteractable
{
    void Interactuar();
    void OnSeleccionado();    // Opcional: para resaltar, mostrar UI, etc.
    void OnDeseleccionado();  // Opcional: quitar resaltado
}