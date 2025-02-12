#![cfg_attr(not(debug_assertions), windows_subsystem = "windows")]

mod db;
mod settings;

use tauri::command;


#[tauri::command]
fn get_encounter_previews(local_player: String) -> Result<Vec<db::BossRecordResult>, String> {
    println!("Received local_player: {}", local_player);
    match db::get_encounter_previews(local_player) {
        Ok(results) => Ok(results),
        Err(e) => Err(e), // Since we're already returning a String error in db, we can just pass it through
    }
}

#[tauri::command]
fn create_settings_file(folder: String) -> Result<String, String> {
    println!("Wrote: {} to settings", folder);
    settings::create_settings_file(folder)  // Pass the input string to the settings function
}

#[tauri::command]
fn read_settings() -> Result<settings::Settings, String> {
    println!("Reading settings from the file.");
    settings::read_settings_file()
}

#[tauri::command]
fn read_local_players(file_path: String) -> Result<settings::PlayerData, String> {
    println!("Reading player data from file: {}.", file_path);
    settings::read_local_players(file_path)
}

fn main() {
    tauri::Builder::default()
        .invoke_handler(tauri::generate_handler![
          get_encounter_previews,
          create_settings_file,
          read_settings,
          read_local_players
      ])
        .run(tauri::generate_context!())
        .expect("error while running tauri application");
}
