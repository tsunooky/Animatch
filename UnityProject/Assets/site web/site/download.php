<?php
// Récupère le nom du fichier à télécharger (vous devez fournir ce nom manuellement)
$filename = 'fichiers_a_telecharger/Epita.png'; // Remplacez 'nom_du_fichier.txt' par le nom réel de votre fichier

// Chemin complet du fichier sur le serveur
$file_path = $_SERVER['DOCUMENT_ROOT']'fichiers_a_telecharger/noob.txt' . $filename;

// Vérifie si le fichier existe
if (file_exists($file_path)) {
    // Envoi des en-têtes pour le téléchargement
    header('Content-Description: File Transfer');
    header('Content-Type: application/octet-stream');
    header('Content-Disposition: attachment; filename="' . basename($file_path) . '"');
    header('Expires: 0');
    header('Cache-Control: must-revalidate');
    header('Pragma: public');
    header('Content-Length: ' . filesize($file_path));

    // Lecture du fichier et envoi du contenu au client
    readfile($file_path);
    exit;
} else {
    // Si le fichier n'existe pas, afficher un message d'erreur
    echo 'Le fichier demandé n\'existe pas.';
}
?>
