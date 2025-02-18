<script lang="ts">
	import { onMount } from 'svelte';
	import { invoke } from '@tauri-apps/api/core';
	import { logsFolder } from '$lib/stores'

	interface Settings {
		logsFolderLocation: string;
	}

	let inputText: string = '';

	async function createSettingsFile() {
		try {
			logsFolder.set(inputText)
			const result = await invoke('create_settings_file', { folder: inputText });
			console.log(result); // Log the result, or you can display a success message

		} catch (error) {
			console.error('Error creating settings file:', error);
		}
	}

	async function getSettings() {
		try {
			const result: Settings = await invoke('read_settings');
			console.log(result.logsFolderLocation);
			logsFolder.set(result.logsFolderLocation);
		} catch (error) {
			console.error('Error querying database:', error);
		}
	}

	onMount(() => {
		if ($logsFolder && $logsFolder === '') {
			getSettings();
		}
		else {
			console.log("logs folder non empty")
		}
	});
</script>

<div>
	<p>
		Please paste your LOA Logs folder path. This can be found at C:\Users\[USER]\AppData\Local\LOA
		Logs. You can also open LOA Logs -> settings -> database and you can open the folder from there.
	</p>
	<p>
		current logs folder: {$logsFolder}
	</p>
	<input
		bind:value={inputText}
		class="input p-2"
		title="Input (text)"
		type="text"
		placeholder="Set LOA Logs location"
	/>
	<button on:click={createSettingsFile} class="btn">Set</button>
</div>
