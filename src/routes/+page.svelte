<script lang="ts">
	import { Accordion, AccordionItem } from '@skeletonlabs/skeleton';
	import { guardianList } from '$lib/guardians';
	import { encounterMap } from '$lib/bosses';
	import { onMount} from 'svelte'

	interface Boss {
    bossName: string;
    bestDPS: number;
  }
	let records: Boss[] = []

	async function getRecords() {
		const response = await fetch('http://localhost:5163/api/EncounterPreview/getRecords?localPlayer=Azenna');
		if (response.ok) {
			records = await response.json()
			console.log("test", records)
		} else {
			console.error('failed to fetch player records')
		}
	}

  onMount(() => {
    getRecords();
  });
</script>

<Accordion>
	<AccordionItem>
		<svelte:fragment slot="lead">(icon)</svelte:fragment>
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
					{#each Object.entries(encounterMap).reverse() as [raidName, encounters]}
						{#each encounters as encounter, i}
							{@const matched = records.find((boss) => boss.bossName === encounter)}
							<tr>
								<td class="border-b px-4 py-2">{raidName} Gate {i + 1}: {encounter}</td>
								{#if matched}
									<td class="border-b px-4 py-2">{Intl.NumberFormat().format(matched.bestDPS)}</td
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
		<svelte:fragment slot="lead">(icon)</svelte:fragment>
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
						{@const matched = records.find((boss) => boss.bossName === guardian)}
						<tr>
							<td class="border-b px-4 py-2">{guardian}</td>
							{#if matched}
								<td class="border-b px-4 py-2">{Intl.NumberFormat().format(matched.bestDPS)}</td>
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
