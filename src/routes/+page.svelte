<script lang="ts">
	import { Accordion, AccordionItem } from '@skeletonlabs/skeleton';
	import { guardianList } from '$lib/guardians';
	import { encounterMap } from '$lib/bosses';

	// Data for the table
	let data = [
		{
			bossName: 'Aegir, the Oppressor',
			bestScore: 46925863
		},
		{
			bossName: 'Akkan, Lord of Death',
			bestScore: 53814061
		},
		{
			bossName: 'Argeos',
			bestScore: 86434848
		},
		{
			bossName: 'Behemoth, Cruel Storm Slayer',
			bestScore: 50483254
		},
		{
			bossName: 'Behemoth, the Storm Commander',
			bestScore: 70602356
		},
		{
			bossName: 'Brelshaza',
			bestScore: 67725
		},
		{
			bossName: 'Chromanium',
			bestScore: 601704
		},
		{
			bossName: 'Covetous Master Echidna',
			bestScore: 66100623
		},
		{
			bossName: 'Dark Mountain Predator',
			bestScore: 7561529
		},
		{
			bossName: 'Evolved Maurug',
			bestScore: 12648788
		},
		{
			bossName: 'Gargadeth',
			bestScore: 35811557
		},
		{
			bossName: 'Icy Legoros+',
			bestScore: 738047
		},
		{
			bossName: 'Kaltaya, the Blooming Chaos',
			bestScore: 35426941
		},
		{
			bossName: 'Killineza the Dark Worshipper',
			bestScore: 29393782
		},
		{
			bossName: 'Lazaram, the Trailblazer',
			bestScore: 34689506
		},
		{
			bossName: 'Lord of Degradation Akkan',
			bestScore: 10841721
		},
		{
			bossName: 'Lord of Kartheon Akkan',
			bestScore: 13541052
		},
		{
			bossName: 'Phantom Legion King',
			bestScore: 85846
		},
		{
			bossName: 'Phantom Legion Queen',
			bestScore: 25053
		},
		{
			bossName: 'Phantom Legion Rook',
			bestScore: 227228
		},
		{
			bossName: 'Rakathus, the Lurking Arrogance',
			bestScore: 61007581
		},
		{
			bossName: 'Red Doom Narkiel',
			bestScore: 46148649
		},
		{
			bossName: 'Sonavel',
			bestScore: 21791463
		},
		{
			bossName: 'Thaemine the Lightqueller',
			bestScore: 46249349
		},
		{
			bossName: 'Tytalos',
			bestScore: 356739
		},
		{
			bossName: 'Valinak, Herald of the End',
			bestScore: 26063443
		},
		{
			bossName: 'Velganos',
			bestScore: 314392
		},
		{
			bossName: 'Veskal',
			bestScore: 33831047
		}
	];
</script>

<Accordion>
	<AccordionItem>
		<svelte:fragment slot="lead">(icon)</svelte:fragment>
		<svelte:fragment slot="summary">Raids</svelte:fragment>
		<svelte:fragment slot="content">
			{#each Object.entries(encounterMap).reverse() as [raidName, encounters]}
				<Accordion>
					<AccordionItem>
						<svelte:fragment slot="lead">(icon)</svelte:fragment>
						<svelte:fragment slot="summary">{raidName}</svelte:fragment>
						<svelte:fragment slot="content">
							<table class="min-w-full table-auto border-collapse">
								<thead>
									<tr>
										<th class="border-b px-4 py-2 text-left font-semibold">Boss Name</th>
										<th class="border-b px-4 py-2 text-left font-semibold">Best DPS</th>
									</tr>
								</thead>
								<tbody>
									{#each encounters as encounter, i}
										{@const matched = data.find((boss) => boss.bossName === encounter)}
										<tr>
											<td class="border-b px-4 py-2">Gate {i + 1}: {encounter}</td>
											{#if matched}
												<td class="border-b px-4 py-2"
													>{Intl.NumberFormat().format(matched.bestScore)}</td
												>
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
			{/each}
		</svelte:fragment>
	</AccordionItem>
</Accordion>

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
							{@const matched = data.find((boss) => boss.bossName === encounter)}
							<tr>
								<td class="border-b px-4 py-2">{raidName} Gate {i + 1}: {encounter}</td>
								{#if matched}
									<td class="border-b px-4 py-2">{Intl.NumberFormat().format(matched.bestScore)}</td
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
						{@const matched = data.find((boss) => boss.bossName === guardian)}
						<tr>
							<td class="border-b px-4 py-2">{guardian}</td>
							{#if matched}
								<td class="border-b px-4 py-2">{Intl.NumberFormat().format(matched.bestScore)}</td>
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
