<script lang="ts">
	import '../app.css';
	import { AppBar } from '@skeletonlabs/skeleton';
	import { initializeStores, Drawer } from '@skeletonlabs/skeleton';
	import { getDrawerStore } from '@skeletonlabs/skeleton';
	import { HomeIcon, SettingsIcon, FileTextIcon } from 'svelte-feather-icons';

	let { children } = $props();
</script>

<div>
	<AppBar gridColumns="grid-cols-3" slotDefault="place-self-center" slotTrail="place-content-end">
		<svelte:fragment slot="lead">
			<div class="flex gap-3">
				<button>
					<div class="flex items-center justify-between gap-2">
						<HomeIcon />
						Home
					</div>
				</button>
				<button>
					<div class="flex items-center justify-between gap-2">
						<FileTextIcon />
						Records
					</div>
				</button>
			</div>
		</svelte:fragment>
		<svelte:fragment slot="trail">
			<button>
				<div class="flex items-center justify-between gap-2">
					<SettingsIcon />
					Settings
				</div>
			</button>
		</svelte:fragment>
	</AppBar>
</div>

{@render children()}
