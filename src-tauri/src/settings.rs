// src-tauri/src/settings.rs

use std::fs;
use std::path::{Path, PathBuf};
use std::env;
use serde::{Serialize, Deserialize};
use std::collections::HashMap;

#[derive(Serialize, Deserialize, Debug)]
pub struct Player {
    pub name: String,
    pub count: u32,
}

#[derive(Serialize, Deserialize, Debug)]
pub struct PlayerData {
    pub clientId: String,
    pub localPlayers: HashMap<String, Player>,
}

#[derive(Serialize, Deserialize)]
pub struct Settings {
    pub logsFolderLocation: String,
}

pub fn create_settings_file(folder: String) -> Result<String, String> {
    // Set the input string to the logsFolderLocation field
    let settings = Settings {
        logsFolderLocation: folder,
    };

    // Get the current working directory (the project folder)
    let project_dir = env::current_dir().map_err(|e| e.to_string())?;

    // Get the parent directory of the current working directory
    let parent_dir = project_dir.parent().ok_or("Failed to get parent directory")?;

    // Specify the path for the settings file in the parent directory
    let settings_path = parent_dir.join("settings.json");

    // Ensure the parent directory exists (in case it doesn't)
    fs::create_dir_all(parent_dir).map_err(|e| e.to_string())?;

    // Serialize the settings into JSON format
    let json_data = serde_json::to_string(&settings).map_err(|e| e.to_string())?;

    // Write the settings JSON data to the file
    fs::write(settings_path.clone(), json_data).map_err(|e| e.to_string())?;

    Ok(format!("Settings file created at {:?}", settings_path))
}

pub fn read_settings_file() -> Result<Settings, String> {
    // Get the current working directory (the project folder)
    let project_dir = env::current_dir().map_err(|e| e.to_string())?;
    
    // Get the parent directory of the current working directory
    let parent_dir = project_dir.parent().ok_or("Failed to get parent directory")?;

    // Specify the path for the settings file in the parent directory
    let settings_path = parent_dir.join("settings.json");

    // Read the content of the settings file
    let json_data = fs::read_to_string(settings_path).map_err(|e| e.to_string())?;

    // Deserialize the JSON data into the Settings struct
    let settings: Settings = serde_json::from_str(&json_data).map_err(|e| e.to_string())?;

    Ok(settings)
}

pub fn read_local_players(file_path: String) -> Result<PlayerData, String> {
    // Read the content of the PlayerData file from the provided file path
    let json_data = fs::read_to_string(file_path).map_err(|e| e.to_string())?;

    // Deserialize the JSON data into the PlayerDatastruct
    let player_data: PlayerData = serde_json::from_str(&json_data).map_err(|e| e.to_string())?;

    Ok(player_data)
}