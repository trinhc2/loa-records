<script lang="ts">
    import { onMount } from 'svelte';

    let folderLocation: string = '';
    let inputText: string = '';

    const backend = 'http://localhost:5163'

    async function getFolderLocation() {
        const response = await fetch(backend + '/api/Settings/getFolderLocation')
        if (response.ok) {
            const data = await response.json()
            folderLocation = data.logsFolderLocation || '';
            inputText = folderLocation
        }
    }

    async function setFolderLocation() {
        const response = await fetch(backend + '/api/Settings/setFolderLocation', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ LogsFolderLocation: inputText }),
    });

    if (response.ok) {
      console.log('Updated JSON');
    } else {
      console.error('Failed to save JSON');
    }

    }
    onMount(() => {
		getFolderLocation();
	});
</script>

<div>
    Settings Page
    <input bind:value={inputText} class="input p-2" title="Input (text)" type="text" placeholder="Set LOA Logs location" />
    <button on:click={setFolderLocation} class="btn">Set</button>
</div>