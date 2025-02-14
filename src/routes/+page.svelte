<script lang="ts">
	import { Accordion, AccordionItem } from '@skeletonlabs/skeleton';
	import { guardianList } from '$lib/guardians';
	import { bossList } from '$lib/bosses';
	import { onMount } from 'svelte';
	import { invoke } from '@tauri-apps/api/core';
	import { writable } from 'svelte/store';

	export const logsFolderLocation = writable('');

	interface Boss {
		boss_name: string;
		best_dps: number;
	}

	interface Player {
		name: String;
		count: Number;
	}

	interface PlayerData {
		clientId: string;
		localPlayers: {
			[playerId: string]: Player;
		};
	}

	interface FolderLocation {
		logsFolderLocation: string;
	}

	let records: Boss[] = [];
	let localPlayers: String[] = [];
	let selectedPlayer: String = '';
	let validFolderPath = false;

	async function getEncounterData(localPlayer: String) {
		try {
			records = await invoke('get_encounter_previews', { localPlayer });
		} catch (error) {
			console.error('Error querying database:', error);
		}
	}

	async function getFolderLocation() {
		try {
			const location: FolderLocation = await invoke('read_settings');
			logsFolderLocation.set(location.logsFolderLocation);
		} catch (error) {
			console.error('error getting folder location:', error);
		}
	}

	async function getLocalPlayers(filePath: String) {
		try {
			console.log('getlocalplayers call:', filePath);
			const result: PlayerData = await invoke('read_local_players', { filePath });
			validFolderPath = true;
			console.log('result', result.localPlayers);
			localPlayers = Object.values(result.localPlayers)
				.sort((a, b) => +b.count - +a.count) // Sort by 'count' in descending order
				.map((player) => player.name);
			console.log(localPlayers);
			selectedPlayer = localPlayers[0];
		} catch (error) {
			console.error('error reading local players:', error);
		}
	}

	onMount(() => {
		getFolderLocation().then(() => {
			console.log('folder location retreived', $logsFolderLocation);
			getLocalPlayers($logsFolderLocation + '\\local_players.json').then(() =>
				getEncounterData(localPlayers[0])
			);
		});
	});
</script>

<div>
	{#if !validFolderPath}
		<p>INVALID FOLDER PATH, PLEASE DOUBLE CHECK YOU SET LOGS FOLDER PATH IN SETTINGS</p>
	{/if}
	<select
		bind:value={selectedPlayer}
		onchange={() => getEncounterData(selectedPlayer)}
		class="select"
	>
		{#each localPlayers as player}
			<option value={player}>{player}</option>
		{/each}
	</select>

	<hr class="mb-3 mt-3 !border-t-2" />

	<Accordion>
		<AccordionItem>
			<svelte:fragment slot="summary">Raids</svelte:fragment>
			<svelte:fragment slot="content">
				<table class="min-w-full table-auto border-collapse">
					<thead>
						<tr>
							<th class="border-b px-4 py-2 text-left font-semibold">Boss Name</th>
							<th class="border-b px-4 py-2 text-left font-semibold">Best DPS</th>
						</tr>
					</thead>
					<tbody>
						{#each Object.entries(bossList).reverse() as [raidName, encounters]}
							{#each encounters as encounter, i}
								{@const matched = records.find((boss) => boss.boss_name === encounter)}
								<tr>
									<td class="border-b px-4 py-2">{raidName} Gate {i + 1}: {encounter}</td>
									{#if matched}
										<td class="border-b px-4 py-2"
											>{Intl.NumberFormat().format(matched.best_dps)}</td
										>
									{:else}
										<td class="border-b px-4 py-2">No Data</td>
									{/if}
								</tr>
							{/each}
						{/each}
					</tbody>
				</table>
			</svelte:fragment>
		</AccordionItem>
	</Accordion>

	<Accordion>
		<AccordionItem>
			<svelte:fragment slot="summary">Guardians</svelte:fragment>
			<svelte:fragment slot="content">
				<table class="min-w-full table-auto border-collapse">
					<thead>
						<tr>
							<th class="border-b px-4 py-2 text-left font-semibold">Boss Name</th>
							<th class="border-b px-4 py-2 text-left font-semibold">Best DPS</th>
						</tr>
					</thead>
					<tbody>
						{#each guardianList as guardian}
							{@const matched = records.find((boss) => boss.boss_name === guardian)}
							<tr>
								<td class="border-b px-4 py-2">{guardian}</td>
								{#if matched}
									<td class="border-b px-4 py-2">{Intl.NumberFormat().format(matched.best_dps)}</td>
								{:else}
									<td class="border-b px-4 py-2">No Data</td>
								{/if}
							</tr>
						{/each}
					</tbody>
				</table>
			</svelte:fragment>
		</AccordionItem>
	</Accordion>
</div>
