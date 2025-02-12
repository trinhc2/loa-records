<script lang="ts">
	import { Accordion, AccordionItem } from '@skeletonlabs/skeleton';
	import { guardianList } from '$lib/guardians';
	import { bossList } from '$lib/bosses';
	import { onMount } from 'svelte';
	import { invoke } from '@tauri-apps/api/core';
	import { writable } from 'svelte/store';

	export const folderLocation = writable('')
	
	interface Boss {
		boss_name: string;
		best_dps: number;
	}

	let records: Boss[] = [];

	async function getEncounterData(localPlayer: String) {
    try {
      records = await invoke('get_encounter_previews', { localPlayer });
    } catch (error) {
      console.error("Error querying database:", error);
    }
  }

  async function getFolderLocation() {
	try {
		const location: string = await invoke('read_settings');
		folderLocation.set(location);
	} catch (error) {
		console.error("error getting folder location:", error)
	}
  }

	onMount(() => {
		getFolderLocation()	
			.then(() => {
				console.log("folder location retreived")
				getEncounterData("Azenna")
			})	
	});
</script>

<div>
	<select class="select">
		<option value="1">Option 1</option>
		<option value="2">Option 2</option>
		<option value="3">Option 3</option>
		<option value="4">Option 4</option>
		<option value="5">Option 5</option>
	</select>

	<hr class="!border-t-2 mt-3 mb-3" />

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
										<td class="border-b px-4 py-2">{Intl.NumberFormat().format(matched.best_dps)}</td>
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
